using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.ServiceModel.Channels;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
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
        private readonly ISecurityAuditLogger _securityAuditLogger;
        private readonly IHostApplicationProvider _hostApplicationProvider;

        public ApplicationAuthenticator(IApplicationValidator applicationValidator,
            IApplicationRepository repository, IServiceCallHeaderParametersProvider parametersProvider,
            ISecurityAuditLogger securityAuditLogger, IHostApplicationProvider hostApplicationProvider)
        {
            if (applicationValidator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "applicationValidator");
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (parametersProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "parametersProvider");
            if (securityAuditLogger == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "securityAuditLogger");
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");

            _applicationValidator = applicationValidator;
            _repository = repository;
            _parametersProvider = parametersProvider;
            _securityAuditLogger = securityAuditLogger;
            _hostApplicationProvider = hostApplicationProvider;
        }

        #endregion

        #region Public methods

        public ReadOnlyCollection<IAuthorizationPolicy> Authenticate(
            ReadOnlyCollection<IAuthorizationPolicy> authPolicy,
            Uri listenUri, ref Message message)
        {
            //Validate client application
            var clientApplication = ValidateClientApplication();

            //Set application principal
            SetApplicationPrincipal(message, clientApplication);
            
            return authPolicy;
        }

        private static void SetApplicationPrincipal(Message message, Application clientApplication)
        {
            var identity = new ServiceIdentity(clientApplication);
            IPrincipal applicationPrincipal = new ApplicationPrincipal(identity);
            message.Properties["Principal"] = applicationPrincipal;
        }

        /// <summary>
        /// Validates client application
        /// 1. ApplicationId passed in message header
        /// 2. Application exists and active
        /// </summary>
        private Application ValidateClientApplication()
        {
            var headerParameters = _parametersProvider.GetParameters();

            var application = _repository.GetApplication(headerParameters.ApplicationId);

            if (application == null)
            {
                var exception = new CoreApiException(TechnicalSubSystem.CoreApiData,
                    SubSystemError.ApplicationNotFoundInDatabase, headerParameters.ApplicationId);

                //Get audit parameters
                var auditParameters = GetAuditParameters(headerParameters);

                //Log security audit failure
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

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
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

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
