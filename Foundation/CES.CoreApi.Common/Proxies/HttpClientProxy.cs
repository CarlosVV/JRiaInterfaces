using System.Net.Http;
using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Common.Proxies
{
    public class HttpClientProxy//: IHttpClientProxy2
    {
        public  static HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}
