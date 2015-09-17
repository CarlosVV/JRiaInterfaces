using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Service.Contract.Constants;
using CES.CoreApi.CustomerVerification.Service.Contract.Models;

namespace CES.CoreApi.CustomerVerification.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.CustomerVerificationServiceContractNamespace)]
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
