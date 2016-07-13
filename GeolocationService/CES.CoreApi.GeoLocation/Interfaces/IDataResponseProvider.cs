using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;


namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IDataResponseProvider
    {
        DataResponse GetResponse(string requestUrl, DataProviderType providerType);
        BinaryDataResponse GetBinaryResponse(string url, DataProviderType providerType);
    }
}