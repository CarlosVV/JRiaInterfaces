using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class PayingAgentLocationRepository : BaseGenericRepository, IPayingAgentLocationRepository
    {
        #region Core

        private readonly ILocationMaterializer _locationMaterializer;
        private readonly ICurrencyMaterializer _currencyMaterializer;

        public PayingAgentLocationRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory,
            IIdentityManager identityManager, IDatabaseInstanceProvider instanceProvider, 
            ILocationMaterializer locationMaterializer, ICurrencyMaterializer currencyMaterializer)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
            if (locationMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "locationMaterializer");
            if (currencyMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "currencyMaterializer");

            _locationMaterializer = locationMaterializer;
            _currencyMaterializer = currencyMaterializer;
        }

        #endregion
        
        #region IPayingAgentLocationRepository implementation

        public AgentLocationModel GetLocation(int agentId, int locationId)
        {
            var request = new DatabaseRequest<AgentLocationModel>
            {
                ProcedureName = "ol_sp_lttblPayAgentsLocs_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fNameIDAgent", agentId)
                    .Add("@fNameIDLoc", locationId),
                Shaper = reader => _locationMaterializer.Materialize(reader, locationId)
            };
            return Get(request);
        }

        public PayingAgentCurrencyModel GetLocationCurrency(int locationId, string currencySymbol)
        {
            var request = new DatabaseRequest<PayingAgentCurrencyModel>
            {
                ProcedureName = "ol_sp_lttblPayAgentsCursLocs_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>()
                    .Add("@fNameIDLoc", locationId)
                    .Add("@fSymbol", currencySymbol),
                Shaper = reader => _currencyMaterializer.Materialize(reader, locationId)
            };

            return Get(request);
        }

        #endregion

    }
}
