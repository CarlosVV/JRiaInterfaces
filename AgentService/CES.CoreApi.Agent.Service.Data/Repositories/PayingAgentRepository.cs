using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class PayingAgentRepository : BaseGenericRepository, IPayingAgentRepository
    {
        #region Core

        private readonly IPayingAgentMaterializer _payingAgentMaterializer;
        private readonly IPayingAgentCurrencyMaterializer _payingAgentCurrencyMaterializer;

        public PayingAgentRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory,
            IIdentityManager identityManager, IDatabaseInstanceProvider instanceProvider,
            IPayingAgentMaterializer payingAgentMaterializer, IPayingAgentCurrencyMaterializer payingAgentCurrencyMaterializer)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
            if (payingAgentCurrencyMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "PayingAgentCurrencyMaterializer");
            if (payingAgentMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "PayingAgentMaterializer");

            _payingAgentMaterializer = payingAgentMaterializer;
            _payingAgentCurrencyMaterializer = payingAgentCurrencyMaterializer;
        }

        #endregion

        public async Task<PayingAgentModel> GetAgent(int agentId)
        {
            var request = new DatabaseRequest<PayingAgentModel>
            {
                ProcedureName = "ol_sp_lttblpayagents_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>().Add("@fNameID", agentId),
                Shaper = reader => _payingAgentMaterializer.Materialize(reader, agentId)
            };
            return await Task.Run(() => Get(request));
        }

        public async Task<PayingAgentCurrencyModel> GetAgentCurrency(int agentId, string currencySymbol)
        {
            var request = new DatabaseRequest<PayingAgentCurrencyModel>
            {
                ProcedureName = "ol_sp_lttblPayAgentsCurs_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fNameID", agentId)
                    .Add("@fSymbol", currencySymbol),
                Shaper = reader => _payingAgentCurrencyMaterializer.Materialize(reader)
            };

            return await Task.Run(() => Get(request));
        }
    }
}
