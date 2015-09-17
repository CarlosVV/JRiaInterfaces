using System.Collections.Generic;
using System.Threading.Tasks;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentLocationRepository
    {
        Task<IEnumerable<PayingAgentLocationModel>> GetLocations(int agentId, int locationId, bool isReceivingAgent);
        Task<PayingAgentCurrencyModel> GetLocationCurrency(int locationId, string currencySymbol);
    }
}