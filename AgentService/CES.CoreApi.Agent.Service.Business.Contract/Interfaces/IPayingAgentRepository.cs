using CES.CoreApi.Agent.Service.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IPayingAgentRepository
    {
        PayingAgentModel GetAgent(int agentId);
        PayingAgentCurrencyModel GetAgentCurrency(int agentId, string currencySymbol);
    }
}