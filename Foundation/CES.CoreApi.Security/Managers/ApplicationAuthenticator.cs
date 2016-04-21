using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.Foundation.Models;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CES.CoreApi.Security.Managers
{
    public class ApplicationAuthenticator : IApplicationAuthenticator
    {
        private readonly IApplicationRepository _repository;
        
        
        public ApplicationAuthenticator(IApplicationRepository repository)
        {
            if (repository == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            
            _repository = repository;
        }

        public IPrincipal Authenticate(ServiceCallHeaderParameters serviceCallHeaderParameters)
        {
            var clientApplication = ValidateClientApplication(serviceCallHeaderParameters).Result;

			var identity = new ClientApplicationIdentity(clientApplication, serviceCallHeaderParameters);
			return new ApplicationPrincipal(identity);
        }
		
        private async Task<Application> ValidateClientApplication(ServiceCallHeaderParameters headerParameters)
        {
            var application = await _repository.GetApplication(headerParameters.ApplicationId);
			if (application == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApiData, SubSystemError.ApplicationNotFoundInDatabase, headerParameters.ApplicationId);

            if (!application.IsActive)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.ClientApplicationDoesNotExistOrInactive, headerParameters.ApplicationId);

            return application;
        }
		
      
    }
}
