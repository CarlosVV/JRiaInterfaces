using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class ReceivingAgentLocationRepository : BaseGenericRepository
    {
        #region Core

        public ReceivingAgentLocationRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory,
            IIdentityManager identityManager, IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        }

        #endregion

        //public async Task<IEnumerable<AgentLocationModel>> GetLocations(int agentId, int locationId, ReceivingAgentDetalizationLevel detalizationLevel)
        //{
        //    //var includeAllLocations = (detalizationLevel & ReceivingAgentDetalizationLevel.AllLocationsWithoutCurrency) == ReceivingAgentDetalizationLevel.AllLocationsWithoutCurrency;

        //    //var request = new DatabaseRequest<PayingAgentLocationModel>
        //    //{
        //    //    ProcedureName = includeAllLocations
        //    //        ? "ol_sp_lttblPayAgentsLocs_GetAllByAgent"
        //    //        : "ol_sp_lttblPayAgentsLocs_Get",
        //    //    IsCacheable = true,
        //    //    DatabaseType = DatabaseType.ReadOnly,
        //    //    Parameters = new Collection<SqlParameter>().Add("@fNameIDAgent", agentId),
        //    //    Shaper = reader => _payingAgentLocationMaterializer.Materialize(reader, locationId),
        //    //    CacheKeySuffix = string.Format(GetLocationsCacheKeySuffixTemplate, detalizationLevel)
        //    //};

        //    //if (!includeAllLocations)
        //    //    request.Parameters.Add("@fNameIDLoc", locationId);

        //    //return await Task.Run(() => GetList(request));
        //}
    }
}
