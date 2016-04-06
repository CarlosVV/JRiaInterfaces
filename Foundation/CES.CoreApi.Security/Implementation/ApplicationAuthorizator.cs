using System;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.Security
{
    public class ApplicationAuthorizator: IApplicationAuthorizator
    {
        private readonly IServiceCallHeaderParametersProvider _parametersProvider;
        private readonly IHostApplicationProvider _hostApplicationProvider;
        private readonly IIdentityManager _identityManager;

        public ApplicationAuthorizator(
            IServiceCallHeaderParametersProvider parametersProvider, IHostApplicationProvider hostApplicationProvider, IIdentityManager identityManager)
        {            
            if (parametersProvider == null)
                throw new CoreApiException(TechnicalSubSystem.Authorization,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "parametersProvider");
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.Authorization,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");
            if (identityManager == null)
                throw new CoreApiException(TechnicalSubSystem.Authorization,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "identityManager");

            _parametersProvider = parametersProvider;
            _hostApplicationProvider = hostApplicationProvider;
            _identityManager = identityManager;
        }
		
        public bool ValidateAccess(OperationContext operationContext)
        {
            var clientApplicationPrincipal = operationContext.IncomingMessageProperties["Principal"] as IPrincipal;

            var clientApplicationIdentity = clientApplicationPrincipal != null
                ? clientApplicationPrincipal.Identity as ClientApplicationIdentity
                : null;

			var clientApplicationId = clientApplicationIdentity != null
				? clientApplicationIdentity.ApplicationId
				: -1;

			var headerParameters = _parametersProvider.GetParameters();
			var hostApplication = _hostApplicationProvider.GetApplication().Result;
			            
            ValidateClientApplicationAuthentication(clientApplicationPrincipal, clientApplicationId);

            ValidateHostApplication(hostApplication);
			ValidateHostApplicationOperation(hostApplication, headerParameters.OperationName);

			ValidateOperationAccess(hostApplication, headerParameters.OperationName, clientApplicationId);

            _identityManager.SetCurrentPrincipal(clientApplicationPrincipal);
            operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = _identityManager.GetCurrentPrincipal();
			
            return true;
        }
        
        private void ValidateClientApplicationAuthentication(IPrincipal clientApplicationPrincipal, int clientApplicationId)
        {
            if (clientApplicationPrincipal != null && clientApplicationPrincipal.Identity.IsAuthenticated)
                return;

            throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.SecurityClientApplicationNotAuthenticated, clientApplicationId);
			
        }

        private void ValidateHostApplication(IApplication hostApplication)
        {
            if (hostApplication?.IsActive == true) 
                return;

            throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.HostApplicationDoesNotExistOrInactive, hostApplication.Id);
        }

        private void ValidateHostApplicationOperation(IApplication hostApplication, string operationName)
        {
            var operation = hostApplication.Operations.FirstOrDefault(
                o => o.Name.Equals(operationName, StringComparison.OrdinalIgnoreCase));

            if (operation == null)
				throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.ServiceOperationNotFound, hostApplication.Id, operationName);
				
            if (!operation.IsActive)
				throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.ServiceOperationIsNotActive, hostApplication.Id, operationName);
        }
		
        private void ValidateOperationAccess(IApplication hostApplication, string operationName, int clientApplicationId)
        {
            var assignedApplication = (from operation in hostApplication.Operations
                where operation.Name.Equals(operationName, StringComparison.OrdinalIgnoreCase)
                let foundOperation = operation
                from appServiceOperation in foundOperation.AssignedApplications
                where appServiceOperation.ApplicationId == clientApplicationId
                select appServiceOperation).FirstOrDefault();

            if (assignedApplication == null)
				throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.SecurityApplicationIsNotAssignedToServiceOperation, clientApplicationId, operationName);
            
            if (!assignedApplication.IsActive)
                throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.SecurityApplicationAssignedToServiceOperationNotActive, clientApplicationId, operationName);
        }
    }
}