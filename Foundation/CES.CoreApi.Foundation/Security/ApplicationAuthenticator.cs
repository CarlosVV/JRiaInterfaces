using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Foundation.Security
{
    public class ApplicationAuthenticator : IApplicationAuthenticator
    {
        #region Core

        private readonly IApplicationValidator _applicationValidator;
        private readonly IApplicationRepository _repository;
        private readonly IServiceCallHeaderParametersProvider _parametersProvider;
        private readonly ISecurityLogMonitor _securityLogMonitor;
        private readonly IHostApplicationProvider _hostApplicationProvider;

        public ApplicationAuthenticator(IApplicationValidator applicationValidator,
            IApplicationRepository repository, IServiceCallHeaderParametersProvider parametersProvider,
            ILogMonitorFactory logMonitorFactory, IHostApplicationProvider hostApplicationProvider)
        {
            if (applicationValidator == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "applicationValidator");
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (parametersProvider == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "parametersProvider");
            if (logMonitorFactory == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "logMonitorFactory");
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");

            _applicationValidator = applicationValidator;
            _repository = repository;
            _parametersProvider = parametersProvider;
            _hostApplicationProvider = hostApplicationProvider;
            _securityLogMonitor = logMonitorFactory.CreateNew<ISecurityLogMonitor>();
        }

        #endregion

        #region Public methods

        public ReadOnlyCollection<IAuthorizationPolicy> Authenticate(
            ReadOnlyCollection<IAuthorizationPolicy> authPolicy,
            Uri listenUri, ref Message message)
        {
            var headerParameters = _parametersProvider.GetParameters();

            //Validate client application
            var clientApplication = ValidateClientApplication(headerParameters).Result;

            //Set application principal
            SetApplicationPrincipal(message, clientApplication, headerParameters);
            
            return authPolicy;
        }

        private static void SetApplicationPrincipal(Message message, 
			IApplication clientApplication, ServiceCallHeaderParameters headerParameters)
        {
            var identity = new ClientApplicationIdentity(clientApplication, headerParameters);
            IPrincipal applicationPrincipal = new ApplicationPrincipal(identity);
            message.Properties["Principal"] = applicationPrincipal;
        }

        /// <summary>
        /// Validates client application
        /// 1. ApplicationId passed in message header
        /// 2. Application exists and active
        /// </summary>
        /// <param name="headerParameters"></param>
        private async Task<Application> ValidateClientApplication(ServiceCallHeaderParameters headerParameters)
        {
            var application = await _repository.GetApplication(headerParameters.ApplicationId);

            if (application == null)
            {
                var exception = new CoreApiException(TechnicalSubSystem.CoreApiData,
                    SubSystemError.ApplicationNotFoundInDatabase, headerParameters.ApplicationId);

                //Get audit parameters
                var auditParameters = GetAuditParameters(headerParameters);

                //Log security audit failure
                _securityLogMonitor.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }

            if (!_applicationValidator.Validate(application))
            {
                var exception = new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.ClientApplicationDoesNotExistOrInactive,
                    headerParameters.ApplicationId);

                //Get audit parameters
                var auditParameters = GetAuditParameters(headerParameters);

                //Log security audit failure
                _securityLogMonitor.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }

            return application;
        }

        /// <summary>
        /// Get parameters for audit logging
        /// </summary>
        /// <param name="headerParameters"></param>
        /// <returns></returns>
        private SecurityAuditParameters GetAuditParameters(ServiceCallHeaderParameters headerParameters)
        {
            //Get host application details
            var hostApplication = _hostApplicationProvider.GetApplication();

            return new SecurityAuditParameters
            {
                ClientApplicationId = headerParameters.ApplicationId,
                Operation = headerParameters.OperationName,
                ServiceApplicationId = hostApplication.Id
            };
        }

        #endregion
    }
}
