﻿using CES.CoreApi.Security.Interfaces;
using System;
using System.Linq;
using System.Security.Principal;
//using CES.CoreApi.Foundation.Contract.Interfaces;
//using CES.CoreApi.Foundation.Contract.Constants;
//using CES.CoreApi.Foundation.Configuration;
//using CES.CoreApi.Foundation.Models;
using CES.CoreApi.Security.Tools;
using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Security.Managers
{
	public class ApplicationAuthorizator: IApplicationAuthorizator
    {
        private readonly IIdentityProvider _identityProvider;
	//	private readonly IApplicationRepository _applicationRepository;

		public ApplicationAuthorizator(IIdentityProvider identityProvider)
        {            
            if (identityProvider == null)
                throw new Exception("TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, \"identityManager\"");

			//if (applicationRepository == null)
			//	throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, "applicationRepository");

			_identityProvider = identityProvider;
			//_applicationRepository = applicationRepository;
		}

		public IPrincipal ValidateAccess(IPrincipal clientApplicationPrincipal)
		{
			var clientApplicationIdentity = clientApplicationPrincipal?.Identity as ClientApplicationIdentity;
			ValidateClientApplicationAuthentication(clientApplicationPrincipal, clientApplicationIdentity?.ApplicationId);

			var applicationId = ConfigurationTools.ReadAppSettingsValueAsInt("ApplicationId");
			if (applicationId == 0)
				throw new Exception("Organization.Ria, TechnicalSystem.CoreApi, TechnicalSubSystem.Authentication, SubSystemError.ApplicationIdNotFoundInConfigFile");
			//ApplicationRepository repo = new ApplicationRepository();
			//var hostApplication = ApplicationRepository.GetApplicationById (applicationId);
			//ValidateHostApplication(hostApplication);

			//ValidateHostApplicationOperation(hostApplication, clientApplicationIdentity?.OperationName);

			//ValidateOperationAccess(hostApplication, clientApplicationIdentity?.OperationName, clientApplicationIdentity?.ApplicationId);

			_identityProvider.SetCurrentPrincipal(clientApplicationPrincipal);

			return _identityProvider.GetCurrentPrincipal();
		}

		private void ValidateClientApplicationAuthentication(IPrincipal clientApplicationPrincipal, int? clientApplicationId)
        {
            if (clientApplicationPrincipal != null && clientApplicationPrincipal.Identity.IsAuthenticated)
                return;

            throw new Exception("TechnicalSubSystem.Authorization, SubSystemError.SecurityClientApplicationNotAuthenticated, clientApplicationId.Value");
        }

        private void ValidateHostApplication(IApplication hostApplication)
        {
            if (hostApplication?.IsActive == true) 
                return;

            throw new Exception("TechnicalSubSystem.Authorization, SubSystemError.HostApplicationDoesNotExistOrInactive, hostApplication.Id");
        }

        private void ValidateHostApplicationOperation(IApplication hostApplication, string operationName)
        {
            var operation = hostApplication.Operations.FirstOrDefault(
                o => o.Name.Equals(operationName, StringComparison.OrdinalIgnoreCase));

            if (operation == null)
				throw new Exception("TechnicalSubSystem.Authorization, SubSystemError.ServiceOperationNotFound, hostApplication.Id, operationName");
				
            if (!operation.IsActive)
				throw new Exception("TechnicalSubSystem.Authorization, SubSystemError.ServiceOperationIsNotActive, hostApplication.Id, operationName");
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
				throw new Exception("TechnicalSubSystem.Authorization, SubSystemError.SecurityApplicationIsNotAssignedToServiceOperation, clientApplicationId, operationName");
            
            if (!assignedApplication.IsActive)
                throw new Exception("TechnicalSubSystem.Authorization, SubSystemError.SecurityApplicationAssignedToServiceOperationNotActive, clientApplicationId, operationName");
        }
	}
}