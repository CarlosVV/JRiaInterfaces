using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class PayingAgentRepository : BaseGenericRepository, IPayingAgentRepository
    {
        #region Core

        private readonly ICurrencyMaterializer _currencyMaterializer;

        public PayingAgentRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory,
            IIdentityManager identityManager, IDatabaseInstanceProvider instanceProvider, 
            ICurrencyMaterializer currencyMaterializer)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
            if (currencyMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "currencyMaterializer");

            _currencyMaterializer = currencyMaterializer;
        }

        #endregion

        public PayingAgentModel GetAgent(int agentId)
        {
            var request = new DatabaseRequest<PayingAgentModel>
            {
                ProcedureName = "ol_sp_lttblpayagents_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>().Add("@fNameID", agentId),
                Shaper = reader => new PayingAgentModel
                {
                    Id = agentId,
                    IsOnHold = reader.ReadValue<bool>("fOnHold"),
                    OnHoldReason = reader.ReadValue<string>("fOnHoldReason"),
                    Status = reader.ReadValue<PayingAgentStatus>("fStatus"),
                    IsBeneficiaryLastName2Required = reader.ReadValue<bool>("fReqBenLastName2")
                }
            };
            return Get(request);
        }

        public PayingAgentCurrencyModel GetAgentCurrency(int agentId, string currencySymbol)
        {
            var request = new DatabaseRequest<PayingAgentCurrencyModel>
            {
                ProcedureName = "ol_sp_lttblPayAgentsCurs_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fNameID", agentId)
                    .Add("@fSymbol", currencySymbol),
                Shaper = reader => _currencyMaterializer.Materialize(reader, agentId)
            };

            return Get(request);
        }
    }
}
