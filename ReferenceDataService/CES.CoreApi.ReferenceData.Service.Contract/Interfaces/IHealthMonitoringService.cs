using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;
using CES.CoreApi.ReferenceData.Service.Contract.Models;

namespace CES.CoreApi.ReferenceData.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.ReferenceDataServiceContractNamespace)]
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
