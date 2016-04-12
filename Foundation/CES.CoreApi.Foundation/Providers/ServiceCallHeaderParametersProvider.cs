using System;
using System.Linq;
using System.ServiceModel;

using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Constants;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Providers
{
    public class ServiceCallHeaderParametersProvider : IServiceCallHeaderParametersProvider
    {
        #region Core

        private readonly IRequestHeadersProvider _headerProvider;
        
        public ServiceCallHeaderParametersProvider(IRequestHeadersProvider headerProvider)
        {
            if (headerProvider == null)
                throw new ArgumentNullException("headerProvider");
            _headerProvider = headerProvider;
        }

        #endregion

        #region public methods

        public ServiceCallHeaderParameters GetParameters()
        {
            var bindingName = OperationContext.Current.EndpointDispatcher.ChannelDispatcher.BindingName;
            var headers = _headerProvider.GetHeaders(bindingName);

            var parameters = new ServiceCallHeaderParameters(
                headers.GetValue<int>(CustomHeaderItems.ApplicationIdHeader),
                GetServiceOperationName(bindingName),
                headers.GetValue<DateTime>(CustomHeaderItems.TimestampHeader),
                headers.GetValue<string>(CustomHeaderItems.ApplicationSessionIdHeader),
                headers.GetValue<string>(CustomHeaderItems.ReferenceNumberHeader),
                headers.GetValue<string>(CustomHeaderItems.ReferenceNumberTypeHeader),
                headers.GetValue<string>(CustomHeaderItems.CorrelationId));
            return parameters;
        }

        #endregion

        #region Private methods

        private static string GetServiceOperationName(string bindingName)
        {
            return bindingName.ToUpper().Contains("WEBHTTPBINDING")
                ? OperationContext.Current.IncomingMessageProperties["HttpOperationName"].ToString()
                : OperationContext.Current.IncomingMessageHeaders.Action.Split(new[] {'/'}).Last();
        }

        #endregion
    }
}
