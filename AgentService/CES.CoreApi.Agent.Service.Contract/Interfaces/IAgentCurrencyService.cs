using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Agent.Service.Contract.Models;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.AgentServiceContractNamespace)]
    public interface IAgentCurrencyService
    {
        [OperationContract(Name = "GetAgentCurrency")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        Task<GetAgentCurrencyResponse> GetAgentCurrency(GetAgentCurrencyRequest request);
    }
}
