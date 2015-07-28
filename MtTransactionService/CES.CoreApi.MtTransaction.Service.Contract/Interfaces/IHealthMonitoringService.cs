using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;
using CES.CoreApi.MtTransaction.Service.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.MtTransactionServiceContractNamespace)]
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
