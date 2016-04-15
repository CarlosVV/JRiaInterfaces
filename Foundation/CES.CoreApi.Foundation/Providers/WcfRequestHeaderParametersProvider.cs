using System;
using System.Linq;
using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Constants;
using CES.CoreApi.Foundation.Contract.Interfaces;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Runtime.Serialization;
using CES.CoreApi.Security;
using System.ServiceModel.Web;

namespace CES.CoreApi.Foundation.Providers
{
    public class WcfRequestHeaderParametersProvider : BaseRequestHeaderParameters, IRequestHeaderParametersProvider
	{
		public ServiceCallHeaderParameters GetParameters()
		{
			var bindingName = OperationContext.Current.EndpointDispatcher.ChannelDispatcher.BindingName;
			var headers = this.GetHeaders(bindingName);

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

		private static string GetServiceOperationName(string bindingName)
		{
			return bindingName.ToUpper().Contains("WEBHTTPBINDING")
				? OperationContext.Current.IncomingMessageProperties["HttpOperationName"].ToString()
				: OperationContext.Current.IncomingMessageHeaders.Action.Split(new[] { '/' }).Last();
		}

		private Dictionary<string, object> GetHeaders(string bindingName)
		{
			var isRestCall = bindingName.IndexOf("WEBHTTPBINDING", StringComparison.OrdinalIgnoreCase) >= 0;
			return isRestCall
				? GetRestHeaders()
				: GetSoapHeaders();
		}

		public Dictionary<string, object> GetHeaders()
		{
			return GetSoapHeaders();
		}

		private Dictionary<string, object> GetSoapHeaders()
		{
			return OperationContext.Current == null
				? new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
				: OperationContext.Current.IncomingMessageHeaders
					.ToList()
					.ToDictionary(p => p.Name,
						p => GetHeader(p.Name, p.Namespace), StringComparer.InvariantCultureIgnoreCase);
		}

		private Dictionary<string, object> GetRestHeaders()
		{
			var headers = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

			if (WebOperationContext.Current == null)
				return headers;

			for (var i = 0; i < WebOperationContext.Current.IncomingRequest.Headers.Count; i++)
			{
				var key = WebOperationContext.Current.IncomingRequest.Headers.GetKey(i);
				var values = WebOperationContext.Current.IncomingRequest.Headers.GetValues(key);
				headers.Add(key, values);
			}

			return headers;
		}

		private object GetHeader(string name, MessageHeaders headers, string ns)
		{
			if (string.IsNullOrEmpty(ns))
				ns = string.Empty;

			var index = headers.FindHeader(name, ns);
			if (index < 0) return null;

			var serializer = new DataContractSerializer(typeof(string), name, ns, null, Int32.MaxValue, false, false, null);
			object headerValue;
			//This should be fixed to get serializer work correctly
			try
			{
				headerValue = headers.GetHeader<object>(index, serializer);
			}
			catch (Exception)
			{
				headerValue = headers[index];
			}
			return headerValue;
		}

		private object GetHeader(string name, string ns)
		{
			return GetHeader(name, OperationContext.Current.IncomingMessageHeaders, ns);
		}
	}
}
