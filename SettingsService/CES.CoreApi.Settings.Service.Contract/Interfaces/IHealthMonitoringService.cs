using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Models;

namespace CES.CoreApi.Settings.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.SettingsServiceContractNamespace)]
    public interface IHealthMonitoringService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        ClearCacheResponse ClearCache();

        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        PingResponse Ping();
    }
}
