using System.Threading.Tasks;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IHealthMonitoringProcessor
    {
        Task<ClearCacheResponseModel> ClearCache();
        Task<PingResponseModel> Ping();
    }
}