using System.Net;

namespace CES.CoreApi.GeoLocation.Models
{
    public class BinaryDataResponse
    {
        public BinaryDataResponse(byte[] rawResponse, HttpStatusCode statusCode, bool isSuccessStatusCode)
        {
            BinaryResponse = rawResponse;
            StatusCode = statusCode;
            IsSuccessStatusCode = isSuccessStatusCode;
        }

        public byte[] BinaryResponse { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public bool IsSuccessStatusCode { get; private set; }
    }
}
