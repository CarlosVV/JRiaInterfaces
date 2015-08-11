using System.Data;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface ILocationMaterializer
    {
        AgentLocationModel Materialize(IDataReader reader, int locationId);
    }
}