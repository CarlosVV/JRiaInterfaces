using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentProcessor
    {
        Task<PayingAgentModel> GetPayingAgent(int agentId, int locationId, string currencySymbol, PayingAgentDetalizationLevel detalizationLevel);
        Task<ReceivingAgentModel> GetReceivingAgent(int agentId, int locationId, ReceivingAgentDetalizationLevel detalizationLevel);
    }
}