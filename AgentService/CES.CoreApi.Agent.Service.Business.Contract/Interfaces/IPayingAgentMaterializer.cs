using System.Data;
using CES.CoreApi.Shared.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IPayingAgentMaterializer
    {
        PayingAgentModel Materialize(IDataReader reader, int agentId);
    }
}