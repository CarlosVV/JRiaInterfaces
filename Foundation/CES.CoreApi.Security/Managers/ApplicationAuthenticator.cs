using System.Security.Principal;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.Security.Interfaces;
using System.Configuration;
using CES.CoreApi.Security.Models;
using CES.CoreApi.Foundation.Models;

namespace CES.CoreApi.Security
{
    public class ApplicationAuthenticator : IApplicationAuthenticator
    {
        private readonly IApplicationRepository _repository;
        private readonly IRequestHeaderParametersProvider _parametersProvider;
        
        public ApplicationAuthenticator(IApplicationRepository repository, IRequestHeaderParametersProviderFactory requestHeaderParametersProviderFactory)
        {
            if (repository == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (requestHeaderParametersProviderFactory == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "requestHeaderParametersProviderFactory");

            _repository = repository;
            _parametersProvider = requestHeaderParametersProviderFactory.GetInstance<IRequestHeaderParametersProvider>(ConfigurationManager.AppSettings["HostServiceType"]);
        }

        public IPrincipal Authenticate()
        {
            var headerParameters = _parametersProvider.GetParameters();
            var clientApplication = ValidateClientApplication(headerParameters).Result;

			var identity = new ClientApplicationIdentity(clientApplication, headerParameters);
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
