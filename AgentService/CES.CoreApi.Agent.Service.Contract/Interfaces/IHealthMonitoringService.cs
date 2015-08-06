using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Agent.Service.Contract.Models;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.AgentServiceContractNamespace)]
    public interface IHealthMonitoringService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        Task<ClearCacheResponse> ClearCache();

        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        Task<PingResponse> Ping();
    }
}