using System;
using System.Linq;
using System.ServiceModel;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Providers
{
    public class ServiceCallHeaderParametersProvider : IServiceCallHeaderParametersProvider
    {
        #region Core

        private readonly IRequestHeadersProvider _headerProvider;

        private const string ApplicationIdHeader = "ApplicationId";
        private const string ApplicationSessionIdHeader = "ApplicationSessionId";
        private const string ReferenceNumberHeader = "ReferenceNumber";
        private const string ReferenceNumberTypeHeader = "ReferenceNumberType";
        private const string TimestampHeader = "Timestamp";

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
                headers.GetValue<int>(ApplicationIdHeader),
                GetServiceOperationName(bindingName),
                headers.GetValue<DateTime>(TimestampHeader),
                headers.GetValue<string>(ApplicationSessionIdHeader),
                headers.GetValue<string>(ReferenceNumberHeader),
                headers.GetValue<string>(ReferenceNumberTypeHeader));
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
