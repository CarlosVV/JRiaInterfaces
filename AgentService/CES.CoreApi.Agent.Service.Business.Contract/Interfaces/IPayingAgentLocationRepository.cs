using System.Collections.Generic;
using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IPayingAgentLocationRepository
    {
        Task<IEnumerable<PayingAgentLocationModel>> GetLocations(int agentId, int locationId, InformationGroup detalizationLevel);
        Task<PayingAgentCurrencyModel> GetLocationCurrency(int locationId, string currencySymbol);
    }
}