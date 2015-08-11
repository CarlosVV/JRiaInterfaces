using System.Data;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Foundation.Data.Utility;

namespace CES.CoreApi.Agent.Service.Data.Materializers
{
    public class CurrencyMaterializer : ICurrencyMaterializer
    {
        public PayingAgentCurrencyModel Materialize(IDataReader reader, int id)
        {
            return new PayingAgentCurrencyModel
            {
                Id = id,
                DailyMaximum = reader.ReadValue<decimal>("fDailyMax"),
                Maximum = reader.ReadValue<decimal>("fMaximum"),
                Minimum = reader.ReadValue<decimal>("fMinimum"),
                Currency = reader.ReadValue<string>("fSymbol")
            };
        }
    }
}
