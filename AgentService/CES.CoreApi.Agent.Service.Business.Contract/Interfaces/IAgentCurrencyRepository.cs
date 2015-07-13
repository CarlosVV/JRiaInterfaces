using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentCurrencyRepository
    {
        PayingAgentCurrencyModel Get(int correspondentId, string symbol);
        DatabasePingModel Ping();
    }
}