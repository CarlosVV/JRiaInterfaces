using System.Data;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Shared.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Data.Materializers
{
    public class PayingAgentCurrencyMaterializer : IPayingAgentCurrencyMaterializer
    {
        public PayingAgentCurrencyModel Materialize(IDataReader reader)
        {
            return new PayingAgentCurrencyModel
            {
                DailyMaximum = reader.ReadValue<decimal>("fDailyMax"),
                Maximum = reader.ReadValue<decimal>("fMaximum"),
                Minimum = reader.ReadValue<decimal>("fMinimum"),
                Currency = reader.ReadValue<string>("fSymbol")
            };
        }
    }
}
