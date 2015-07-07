using CES.CoreApi.Common.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IHealthMonitoringProcessor
    {
        ClearCacheResponseModel ClearCache();
        PingResponseModel Ping();
    }
}