using CES.Security.CoreApi.Models;
using System.Net.Http;

namespace CES.CoreApi.Compliance.Screening.Utilities
{
    public class Client
    {
        public static ClientApplicationIdentity Identity
        {
            get
            {
                return System.Threading.Thread.CurrentPrincipal.Identity as ClientApplicationIdentity;
            }
        }
		public static object GetCorrelationId(HttpRequestMessage request)
		{
			var correlationIds = null as System.Collections.Generic.IEnumerable<string>;
			if (request.Headers.TryGetValues("CorrelationId", out correlationIds))
			{
				var key = string.Join("", correlationIds);
				if (!string.IsNullOrEmpty(key))
					return key;
			}
			return request.GetCorrelationId();
		}
	}
}