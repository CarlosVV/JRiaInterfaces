using System.Data;
using CES.CoreApi.Shared.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface IReceivingAgentMaterializer
    {
        ReceivingAgentModel Materialize(IDataReader reader, int agentId);
    }
}