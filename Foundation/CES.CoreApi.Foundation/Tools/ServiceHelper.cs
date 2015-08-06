using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Proxies;
using CES.CoreApi.Foundation.Contract.Constants;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Tools
{
    public class ServiceHelper : IServiceHelper
    {
        #region Core

        private readonly IServiceConfigurationProvider _configurationProvider;
        private readonly IIdentityManager _identityManager;

        public ServiceHelper(IServiceConfigurationProvider configurationProvider, IIdentityManager identityManager)
        {
            if (configurationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
            if (identityManager == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "identityManager");

            _configurationProvider = configurationProvider;
            _identityManager = identityManager;
        }

        #endregion

        #region Public methods

        public TResult Execute<TService, TResult>(Func<TService, TResult> serviceDelegate)
            where TService : class
        {
            var endpoint = GetEndpoint<TService>();

            using (var proxy = new ServiceProxy<TService>(endpoint.ConfiguredBinding, endpoint.Address))
            {
                var result = proxy.Execute(serviceDelegate, AddCustomHeaders);
                return result;
            }
        }

        public async Task<TResult> ExecuteAsync<TService, TResult>(Func<TService, TResult> serviceDelegate)
           where TService : class
        {
            var endpoint = GetEndpoint<TService>();

            using (var proxy = new ServiceProxy<TService>(endpoint.ConfiguredBinding, endpoint.Address))
            {
                return await proxy.ExecuteAsync(serviceDelegate, AddCustomHeaders);
            }
        }

        #endregion

        #region Private methods

        private ServiceEndpointConfiguration GetEndpoint<TService>()
        {
            var serviceConfiguration = _configurationProvider.GetConfiguration(ServiceConfigurationType.ClientEndpoints);

            var endpoint = serviceConfiguration.Endpoints.FirstOrDefault(
                p => p.Contract.Equals(typeof (TService).FullName, StringComparison.OrdinalIgnoreCase));

            if (endpoint == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationNoClientEndpointFound, typeof (TService).FullName);

            return endpoint;
        }

        private void AddCustomHeaders()
        {
            var applicationIdentity = _identityManager.GetClientApplicationIdentity();

            AddHeader(CustomHeaderItems.ApplicationIdHeader, applicationIdentity.ApplicationId);
            AddHeader(CustomHeaderItems.TimestampHeader, applicationIdentity.Timestamp);
            AddHeader(CustomHeaderItems.ApplicationSessionIdHeader, applicationIdentity.ApplicationSessionId);
            AddHeader(CustomHeaderItems.ReferenceNumberHeader, applicationIdentity.ReferenceNumber);
            AddHeader(CustomHeaderItems.ReferenceNumberTypeHeader, applicationIdentity.ReferenceNumberType);
        }

        private static void AddHeader<T>(string name, T value)
        {
            var appHeader = new MessageHeader<T>(value);
            OperationContext.Current.OutgoingMessageHeaders.Add(appHeader.GetUntypedHeader(name, Namespaces.MessageHeaderNamespace));
        }

        #endregion
    }
}
