using System.Net.Http;

namespace CES.CoreApi.Common.Proxies
{
    public class HttpClientProxy
    {
        public  static HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}
