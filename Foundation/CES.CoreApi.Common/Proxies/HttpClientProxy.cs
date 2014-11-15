using System.Net.Http;
using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Common.Proxies
{
    public class HttpClientProxy : IHttpClientProxy
    {
        public HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}
