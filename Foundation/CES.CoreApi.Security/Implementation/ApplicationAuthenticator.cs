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
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Security
{
    public class ApplicationAuthenticator : IApplicationAuthenticator
    {
        private readonly IApplicationRepository _repository;
        private readonly IServiceCallHeaderParametersProvider _parametersProvider;
        
        public ApplicationAuthenticator(IApplicationRepository repository, IServiceCallHeaderParametersProvider parametersProvider, IHostApplicationProvider hostApplicationProvider)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (parametersProvider == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "parametersProvider");
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");

            _repository = repository;
            _parametersProvider = parametersProvider;
        }

        public ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
        {
            var headerParameters = _parametersProvider.GetParameters();
            var clientApplication = ValidateClientApplication(headerParameters).Result;
            SetApplicationPrincipal(message, clientApplication, headerParameters);
            
            return authPolicy;
        }

        private static void SetApplicationPrincipal(Message message, IApplication clientApplication, ServiceCallHeaderParameters headerParameters)
        {
            var identity = new ClientApplicationIdentity(clientApplication);
            IPrincipal applicationPrincipal = new ApplicationPrincipal(identity);
            message.Properties["Principal"] = applicationPrincipal;
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
