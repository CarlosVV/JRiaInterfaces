using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Constants.Namespaces.GeolocationServiceContractNamespace)]
    public interface IHealthMonitoringService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        ClearCacheResponse ClearCache();

        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        PingResponse Ping();
    }
}