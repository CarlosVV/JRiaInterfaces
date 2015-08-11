using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IPayingAgentLocationRepository
    {
        AgentLocationModel GetLocation(int agentId, int locationId);
        PayingAgentCurrencyModel GetLocationCurrency(int locationId, string currencySymbol);
    }
}