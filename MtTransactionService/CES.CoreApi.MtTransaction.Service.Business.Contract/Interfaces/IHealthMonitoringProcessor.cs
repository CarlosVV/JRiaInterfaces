using CES.CoreApi.Common.Models;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Interfaces
{
    public interface IHealthMonitoringProcessor
    {
        ClearCacheResponseModel ClearCache();
        PingResponseModel Ping();
    }
}