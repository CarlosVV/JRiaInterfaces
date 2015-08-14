using System.Threading.Tasks;
using CES.CoreApi.Shared.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IReceivingAgentRepository
    {
        Task<ReceivingAgentModel> GetAgent(int agentId);
    }
}