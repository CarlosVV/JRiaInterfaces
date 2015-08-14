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
using CES.CoreApi.Shared.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class ReceivingAgentRepository: BaseGenericRepository, IReceivingAgentRepository
    {
        #region Core

        private readonly IReceivingAgentMaterializer _receivingAgentMaterializer;

        public ReceivingAgentRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager,
           IDatabaseInstanceProvider instanceProvider, IReceivingAgentMaterializer receivingAgentMaterializer) 
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
            if (receivingAgentMaterializer == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "receivingAgentMaterializer");

            _receivingAgentMaterializer = receivingAgentMaterializer;
        }

        #endregion

        public async Task<ReceivingAgentModel> GetAgent(int agentId)
        {
            var request = new DatabaseRequest<ReceivingAgentModel>
            {
                ProcedureName = "ol_sp_lttblRecAgents_Get",
                IsCacheable = true,
                DatabaseType = DatabaseType.ReadOnly,
                Parameters = new Collection<SqlParameter>().Add("@fRecAgentID", agentId),
                Shaper = reader => _receivingAgentMaterializer.Materialize(reader, agentId)
            };
            return await Task.Run(() => Get(request));
        }
    }
}
