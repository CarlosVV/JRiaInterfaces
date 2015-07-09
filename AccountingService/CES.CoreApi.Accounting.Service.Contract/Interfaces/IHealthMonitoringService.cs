using System.ServiceModel;
using CES.CoreApi.Accounting.Service.Contract.Constants;
using CES.CoreApi.Accounting.Service.Contract.Models;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.AccountingServiceContractNamespace)]
    public interface IHealthMonitoringService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        ClearCacheResponse ClearCache();

        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        HealthMonitoringResponse Ping();
    }
}