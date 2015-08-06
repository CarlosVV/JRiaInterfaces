using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IAgentCurrencyProcessor
    {
        Task<PayingAgentCurrencyModel> GetAgentCurrent(int correspondentId, string symbol);
    }
}