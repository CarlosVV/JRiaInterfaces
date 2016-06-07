//using CES.CoreApi.Foundation.Contract.Models;
//using CES.CoreApi.Foundation.Models;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Models;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CES.CoreApi.Security.Managers
{
	public class ApplicationAuthenticator : IApplicationAuthenticator
    {
		
		public async Task<IPrincipal> AuthenticateAsync(ServiceCallHeaderParameters serviceCallHeaderParameters)
		{
			if (serviceCallHeaderParameters == null)
				//throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "serviceCallHeaderParameters");
				throw new Exception("TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined:serviceCallHeaderParameters");
			var clientApplication = await ValidateClientApplicationAsync(serviceCallHeaderParameters);

			var identity = new ClientApplicationIdentity(clientApplication, serviceCallHeaderParameters);
			return new ApplicationPrincipal(identity);
		}

		private async Task<Application> ValidateClientApplicationAsync(ServiceCallHeaderParameters headerParameters)
		{
			if (headerParameters.ApplicationId < 1)
				throw new Exception("Application id is required ");
			ApplicationRepository applicationRepository = new ApplicationRepository();
			var application = await applicationRepository.GetApplication(headerParameters.ApplicationId);
			if (application == null)
				throw new Exception("TechnicalSubSystem.CoreApiData, SubSystemError.ApplicationNotFoundInDatabase, headerParameters.ApplicationId");

			if (!application.IsActive)
				throw new Exception("TechnicalSubSystem.Authentication, SubSystemError.ClientApplicationDoesNotExistOrInactive, headerParameters.ApplicationId");

			return application;
		}

		public IPrincipal Authenticate(ServiceCallHeaderParameters serviceCallHeaderParameters)
        {
			if (serviceCallHeaderParameters == null)
				//throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "serviceCallHeaderParameters");
				throw new Exception("TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined:serviceCallHeaderParameters");
            var clientApplication =  ValidateClientApplication(serviceCallHeaderParameters);

			var identity = new ClientApplicationIdentity(clientApplication, serviceCallHeaderParameters);
			return new ApplicationPrincipal(identity);
        }
		
        private Application ValidateClientApplication(ServiceCallHeaderParameters headerParameters)
        {
			//ApplicationRepository applicationRepository = new ApplicationRepository();
			if(headerParameters.ApplicationId <1)
				throw new Exception("Application id is required ");
			var application = ApplicationRepository.GetApplicationById(headerParameters.ApplicationId);
			if (application == null)
                throw new Exception("TechnicalSubSystem.CoreApiData, SubSystemError.ApplicationNotFoundInDatabase, headerParameters.ApplicationId");

            if (!application.IsActive)
				throw new Exception("TechnicalSubSystem.Authentication, SubSystemError.ClientApplicationDoesNotExistOrInactive, headerParameters.ApplicationId");

            return application;
        }
    }
}
