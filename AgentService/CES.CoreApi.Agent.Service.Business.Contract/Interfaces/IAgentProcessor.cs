using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentProcessor
    {
        Task<PayingAgentCurrencyModel> GetAgentCurrency(int agentId, string currencySymbol);
        Task<GetAgentResponseResponseModel> GetPayingAgent(int agentId, int locationId, InformationGroup detalizationLevel);
    }
}