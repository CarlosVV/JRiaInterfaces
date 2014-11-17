using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IClientSideSupportServiceProcessor
    {
        GetProviderKeyResponseModel GetProviderKey(DataProviderType providerType);
        void LogEvent(DataProviderType providerType, string message);
    }
}