using System;
using CES.CoreApi.Foundation.Models;
using System.Web;
using System.Collections.Generic;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Constants;
using CES.CoreApi.Security.WebApi.Interfaces;

namespace CES.CoreApi.Security.Providers
{
	public class WebApiRequestHeaderParametersService: IWebApiRequestHeaderParametersService
	{
        public ServiceCallHeaderParameters GetParameters(string operationName)
		{
			var headers = this.GetHeaders();
			var parameters = new ServiceCallHeaderParameters(
				headers.GetValue<int>(CustomHeaderItems.ApplicationIdHeader),
				operationName,
				headers.GetValue<DateTime>(CustomHeaderItems.TimestampHeader),
				headers.GetValue<string>(CustomHeaderItems.ApplicationSessionIdHeader),
				headers.GetValue<string>(CustomHeaderItems.ReferenceNumberHeader),
				headers.GetValue<string>(CustomHeaderItems.ReferenceNumberTypeHeader),
				headers.GetValue<string>(CustomHeaderItems.CorrelationId));
			return parameters;
		}

		private Dictionary<string, object> GetHeaders()
		{
			var headers = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);

			if (HttpContext.Current == null)
				return headers;

			for (var i = 0; i < HttpContext.Current.Request.Headers.Count; i++)
			{
				var key = HttpContext.Current.Request.Headers.GetKey(i);
				var values = HttpContext.Current.Request.Headers.GetValues(key)[0];
				headers.Add(key, values);
			}
			
			return headers;
		}
	}
}
