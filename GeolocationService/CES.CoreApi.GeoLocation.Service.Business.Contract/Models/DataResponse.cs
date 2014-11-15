using System.Net;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Models
{
    public class DataResponse
    {
        public DataResponse(string rawResponse, HttpStatusCode statusCode, bool isSuccessStatusCode)
        {
            RawResponse = rawResponse;
            StatusCode = statusCode;
            IsSuccessStatusCode = isSuccessStatusCode;
        }

        public string RawResponse { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public bool IsSuccessStatusCode { get; private set; }
    }
}
