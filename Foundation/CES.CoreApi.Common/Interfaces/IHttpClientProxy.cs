using System.Net.Http;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IHttpClientProxy2
    {
        HttpClient GetHttpClient();
    }
}