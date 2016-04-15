using System;
using System.Linq;
using System.Security.Principal;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security.Interfaces;
using System.Configuration;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Security
{
    public class ApplicationAuthorizator: IApplicationAuthorizator
    {
        private readonly IRequestHeaderParametersProvider _parametersProvider;
        private readonly IHostApplicationProvider _hostApplicationProvider;
        private readonly IIdentityManager _identityManager;

        public ApplicationAuthorizator(
			IRequestHeaderParametersProviderFactory requestHeaderParametersProviderFactory, IHostApplicationProvider hostApplicationProvider, IIdentityManager identityManager)
        {            
            if (requestHeaderParametersProviderFactory == null)
                throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, "requestHeaderParametersProviderFactory");
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");
            if (identityManager == null)
                throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, "identityManager");

            _parametersProvider = requestHeaderParametersProviderFactory.GetInstance<IRequestHeaderParametersProvider>(ConfigurationManager.AppSettings["HostServiceType"]);
            _hostApplicationProvider = hostApplicationProvider;
            _identityManager = identityManager;
        }
		
        public IPrincipal ValidateAccess(IPrincipal clientApplicationPrincipal)
        {
			int? clientApplicationId = (clientApplicationPrincipal?.Identity as ClientApplicationIdentity)?.ApplicationId;
			ValidateClientApplicationAuthentication(clientApplicationPrincipal, clientApplicationId);

			var hostApplication = _hostApplicationProvider.GetApplication().Result;
			ValidateHostApplication(hostApplication);

			var headerParameters = _parametersProvider.GetParameters();
			ValidateHostApplicationOperation(hostApplication, headerParameters.OperationName);

			ValidateOperationAccess(hostApplication, headerParameters.OperationName, clientApplicationId);

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