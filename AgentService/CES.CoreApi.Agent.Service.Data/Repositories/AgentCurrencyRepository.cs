using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class AgentCurrencyRepository : BaseGenericRepository, IAgentCurrencyRepository
    {
        #region Core

        public AgentCurrencyRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager, 
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        } 

        #endregion

        #region IAgentCurrencyRepository implementation

        public PayingAgentCurrencyModel Get(int correspondentId, string symbol)
        {
            var request = new DatabaseRequest<PayingAgentCurrencyModel>
            {
                ProcedureName = "ol_sp_lttblPayAgentsCurs_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fNameID", correspondentId)
                    .Add("@fSymbol", symbol),
                Shaper = reader => new PayingAgentCurrencyModel
                {
                    DailyMaximum = reader.ReadValue<decimal>("fDailyMax"),
                    Maximum = reader.ReadValue<decimal>("fMaximum"),
                    Minimum = reader.ReadValue<decimal>("fMinimum"),
                    Id = reader.ReadValue<int>("fNameID"),
                    Currency = reader.ReadValue<string>("fSymbol")
                }
            };

            return Get(request);
        }

        #endregion
    }
}
