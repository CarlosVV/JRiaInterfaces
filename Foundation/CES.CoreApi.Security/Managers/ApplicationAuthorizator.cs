using CES.CoreApi.Security.Interfaces;
using System;
using System.Linq;
using System.Security.Principal;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Security.Models;
using CES.CoreApi.Foundation.Contract.Constants;
using System.Configuration;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Configuration;

namespace CES.CoreApi.Security
{
    public class ApplicationAuthorizator: IApplicationAuthorizator
    {
        private readonly IIdentityManager _identityManager;
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationAuthorizator(IIdentityManager identityManager, IApplicationRepository applicationRepository)
        {            
            if (identityManager == null)
                throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, "identityManager");

			if (applicationRepository == null)
				throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, "applicationRepository");

			_identityManager = identityManager;
			_applicationRepository = applicationRepository;
		}
		
        public IPrincipal ValidateAccess(IPrincipal clientApplicationPrincipal)
        {
			var clientApplicationIdentity = clientApplicationPrincipal?.Identity as ClientApplicationIdentity;
			ValidateClientApplicationAuthentication(clientApplicationPrincipal, clientApplicationIdentity?.ApplicationId);

			var applicationId = ConfigurationTools.ReadAppSettingsValue<int>(ServiceConfigurationItems.ApplicationId);
			if (applicationId == 0)
				throw new CoreApiException(Organization.Ria, TechnicalSystem.CoreApi, TechnicalSubSystem.Authentication, SubSystemError.ApplicationIdNotFoundInConfigFile);

			var hostApplication = _applicationRepository.GetApplication(applicationId).Result;
			ValidateHostApplication(hostApplication);

			ValidateHostApplicationOperation(hostApplication, clientApplicationIdentity?.OperationName);

			ValidateOperationAccess(hostApplication, clientApplicationIdentity?.OperationName, clientApplicationIdentity?.ApplicationId);

			_identityManager.SetCurrentPrincipal(clientApplicationPrincipal);
			
			return _identityManager.GetCurrentPrincipal();
        }
        
        private void ValidateClientApplicationAuthentication(IPrincipal clientApplicationPrincipal, int? clientApplicationId)
        {
            if (clientApplicationPrincipal != null && clientApplicationPrincipal.Identity.IsAuthenticated)
                return;

            throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.SecurityClientApplicationNotAuthenticated, clientApplicationId.Value);
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
		
        private void ValidateOperationAccess(IApplication hostApplication, string operationName, int? clientApplicationId)
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