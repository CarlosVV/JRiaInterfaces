using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Customer.Service.Business.Contract.Interfaces
{
    public interface IHealthMonitoringProcessor
    {
        ClearCacheResponseModel ClearCache();
        HealthResponseModel Ping();
    }
}