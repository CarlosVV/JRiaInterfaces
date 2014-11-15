using System.Net.Http;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IHttpClientProxy
    {
        HttpClient GetHttpClient();
    }
}