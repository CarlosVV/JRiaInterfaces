using System.Data;
using CES.CoreApi.Agent.Service.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Interfaces
{
    public interface ICurrencyMaterializer
    {
        PayingAgentCurrencyModel Materialize(IDataReader reader, int id);
    }
}