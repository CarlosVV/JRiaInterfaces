using System;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Foundation.Security
{
    public class AuthorizationAdministrator : IAuthorizationAdministrator
    {
        #region Core

        private readonly IApplicationValidator _applicationValidator;
        private readonly IServiceCallHeaderParametersProvider _parametersProvider;
        private readonly IHostApplicationProvider _hostApplicationProvider;
        private readonly ISecurityAuditLogger _securityAuditLogger;

        public AuthorizationAdministrator(IApplicationValidator applicationValidator,
            IServiceCallHeaderParametersProvider parametersProvider, IHostApplicationProvider hostApplicationProvider, 
            ISecurityAuditLogger securityAuditLogger)
        {
            if (applicationValidator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "applicationValidator");
            if (parametersProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "parametersProvider");
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");
            if (securityAuditLogger == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "securityAuditLogger");

            _applicationValidator = applicationValidator;
            _parametersProvider = parametersProvider;
            _hostApplicationProvider = hostApplicationProvider;
            _securityAuditLogger = securityAuditLogger;
        }

        #endregion

        #region Public methods

        public bool ValidateAccess(OperationContext operationContext)
        {
            var clientApplicationPrincipal = operationContext.IncomingMessageProperties["Principal"] as IPrincipal;

            var clientApplicationIdentity = clientApplicationPrincipal != null
                ? clientApplicationPrincipal.Identity as ServiceIdentity
                : null;

            var clientApplicationId = clientApplicationIdentity != null
                ? clientApplicationIdentity.ApplicationId
                : -1;

            //Get service call header parameters
            var headerParameters = _parametersProvider.GetParameters();

            //Get host application details
            var hostApplication = _hostApplicationProvider.GetApplication();

            var auditParameters = new SecurityAuditParameters
            {
                ClientApplicationId = clientApplicationId,
                Operation = headerParameters.OperationName,
                ServerId = hostApplication.ServerId,
                ServiceApplicationId = hostApplication.Id
            };
            
            //Check that client application was authenticated
            ValidateClientApplicationAuthentication(clientApplicationPrincipal, clientApplicationId, auditParameters);
            
            //Validate host application existence and IsActive status
            ValidateHostApplication(hostApplication, auditParameters);

            //Validate host service operation
            ValidateHostApplicationOperation(hostApplication, headerParameters.OperationName, auditParameters);

            //Validate that client application has access to requested operation
            ValidateOperationAccess(hostApplication, headerParameters.OperationName, clientApplicationId, auditParameters);

            //Set principal
            Thread.CurrentPrincipal = clientApplicationPrincipal;
            operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = Thread.CurrentPrincipal;

            //Log security audit success
            _securityAuditLogger.LogSuccess(auditParameters);

            return true;
        }
        
        #endregion

        #region Private methods

        /// <summary>
        /// Validates that client application was authenticated
        /// </summary>
        /// <param name="clientApplicationPrincipal">Client application principal</param>
        /// <param name="clientApplicationId">Client application identifier</param>
        /// <param name="auditParameters">Security audit parameters</param>
        private void ValidateClientApplicationAuthentication(IPrincipal clientApplicationPrincipal, int clientApplicationId, SecurityAuditParameters auditParameters)
        {
            if (clientApplicationPrincipal != null && clientApplicationPrincipal.Identity.IsAuthenticated)
                return;

            var exception = new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.SecurityClientApplicationNotAuthenticated, clientApplicationId);

            //Log security audit failure
            _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

            throw exception;
        }

        /// <summary>
        /// Validate host service
        /// 1. Service exist
        /// 2. Service IsActive status = true
        /// 3. Service exsist on particular server and IsActive status there = true
        /// </summary>
        /// <param name="hostApplication"></param>
        /// <param name="auditParameters"></param>
        private void ValidateHostApplication(IHostApplication hostApplication, SecurityAuditParameters auditParameters)
        {
            if (!_applicationValidator.Validate(hostApplication as Application))
            {
                var exception =  new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.HostApplicationDoesNotExistOrInactive,
                    hostApplication.Id);

                //Log security audit failure
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }

            //Validate host application IsActive status on particular server
            var server = hostApplication.Servers.FirstOrDefault(s => s.Id == hostApplication.ServerId && s.IsActive);
            if (server == null)
            {
                var exception = new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.HostApplicationDoesNotExistOrInactiveOnServer,
                    hostApplication.Id, hostApplication.ServerId);

                //Log security audit failure
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }
        }

        /// <summary>
        /// Validates host service operation
        /// 1. Host service operation exists
        /// 2. Host service operation active
        /// </summary>
        /// <param name="hostApplication">Host application instance</param>
        /// <param name="operationName">Operation name to validate</param>
        /// <param name="auditParameters"></param>
        private void ValidateHostApplicationOperation(IHostApplication hostApplication, string operationName, SecurityAuditParameters auditParameters)
        {
            //Validate operation existence
            var operation = hostApplication.Operations.FirstOrDefault(
                o => o.Name.Equals(operationName, StringComparison.OrdinalIgnoreCase));

            if (operation == null)
            {
                var exception = new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.ServiceOperationNotFound,
                    hostApplication.Id, operationName);

                //Log security audit failure
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }

            //Check if operation is active
            if (!operation.IsActive)
            {
                var exception = new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.ServiceOperationIsNotActive,
                    hostApplication.Id, operationName);

                //Log security audit failure
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }
        }

        /// <summary>
        /// Validates client application access to host service method
        /// 1. Client application assigned to host service method
        /// 2. Client applicaiton assignment to host service method is active
        /// </summary>
        /// <param name="hostApplication">Host service application</param>
        /// <param name="operationName">Host service application method name</param>
        /// <param name="clientApplicationId">Client application identifier</param>
        /// <param name="auditParameters"></param>
        private void ValidateOperationAccess(IHostApplication hostApplication, string operationName, int clientApplicationId, SecurityAuditParameters auditParameters)
        {
            var assignedApplication = (from operation in hostApplication.Operations
                where operation.Name.Equals(operationName, StringComparison.OrdinalIgnoreCase)
                let foundOperation = operation
                from appServiceOperation in foundOperation.AssignedApplications
                where appServiceOperation.ApplicationId == clientApplicationId
                select appServiceOperation).FirstOrDefault();

            if (assignedApplication == null)
            {
                var exception = new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.SecurityApplicationIsNotAssignedToServiceOperation,
                    hostApplication.Id, operationName);

                //Log security audit failure
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }

            if (!assignedApplication.IsActive)
            {
                var exception = new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.SecurityApplicationAssignedToServiceOperationNotActive,
                    hostApplication.Id, operationName);

                //Log security audit failure
                _securityAuditLogger.LogFailure(auditParameters, exception.ClientMessage);

                throw exception;
            }
        }
    }

    #endregion
}