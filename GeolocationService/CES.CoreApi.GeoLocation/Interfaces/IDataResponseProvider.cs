using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;


namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IDataResponseProvider
    {
        DataResponse GetResponse(string requestUrl);
        BinaryDataResponse GetBinaryResponse(string url);
    }
}