using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IHealthMonitoringProcessor
    {
        ClearCacheResponseModel ClearCache();
        PingResponseModel Ping();
    }
}