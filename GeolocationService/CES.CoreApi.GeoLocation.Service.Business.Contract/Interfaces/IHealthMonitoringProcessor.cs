

using CES.CoreApi.Common.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IHealthMonitoringProcessor
    {
		ClearCacheResponseModel ClearCache();
		object Ping();
    }
}