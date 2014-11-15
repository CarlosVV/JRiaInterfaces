using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IDataResponseProvider
    {
        DataResponse GetResponse(string requestUrl, DataProviderType providerType);
        BinaryDataResponse GetBinaryResponse(string url, DataProviderType providerType);
    }
}