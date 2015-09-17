using System.Collections.Generic;
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
    public class AgentLocationRepository : BaseGenericRepository, IAgentLocationRepository
    {
        #region Core

        private readonly IAgentLocationMaterializer _agentLocationMaterializer;
        private readonly IPayingAgentCurrencyMaterializer _payingAgentCurrencyMaterializer;

        public AgentLocationRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory,
            IIdentityManager identityManager, IDatabaseInstanceProvider instanceProvider, 
            IAgentLocationMaterializer agentLocationMaterializer, IPayingAgentCurrencyMaterializer payingAgentCurrencyMaterializer)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
            if (agentLocationMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "agentLocationMaterializer");
            if (payingAgentCurrencyMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "PayingAgentCurrencyMaterializer");

            _agentLocationMaterializer = agentLocationMaterializer;
            _payingAgentCurrencyMaterializer = payingAgentCurrencyMaterializer;
        }

        #endregion
        
        #region IAgentLocationRepository implementation

        public async Task<IEnumerable<PayingAgentLocationModel>> GetLocations(int agentId, int locationId, bool isReceivingAgent)
        {
            var request = new DatabaseRequest<PayingAgentLocationModel>
            {
                ProcedureName = "coreapi_sp_GetAgentAddress",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@AgentID", agentId)
                    .Add("@LocationID", locationId)
                    .Add("@IsReceivingAgent", isReceivingAgent ? 1 : 0),
                Shaper = reader => _agentLocationMaterializer.MaterializePayingAgentLocation(reader)
            };

            return await Task.Run(() => GetList(request));
        }

        public async Task<PayingAgentCurrencyModel> GetLocationCurrency(int locationId, string currencySymbol)
        {
            var request = new DatabaseRequest<PayingAgentCurrencyModel>
            {
                ProcedureName = "ol_sp_lttblPayAgentsCursLocs_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fNameIDLoc", locationId)
                    .Add("@fSymbol", currencySymbol),
                Shaper = reader => _payingAgentCurrencyMaterializer.Materialize(reader)
            };

            return await Task.Run(() => Get(request));
        }

        #endregion

    }
}
