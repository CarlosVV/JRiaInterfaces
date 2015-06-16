using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Settings.Service.Business.Contract.Interfaces
{
    public interface IHealthMonitoringProcessor
    {
        ClearCacheResponseModel ClearCache();
        HealthResponseModel Ping();
    }
}