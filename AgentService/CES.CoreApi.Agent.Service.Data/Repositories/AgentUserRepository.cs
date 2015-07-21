using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class AgentUserRepository: BaseGenericRepository, IAgentUserRepository
    {
        #region Core

        public AgentUserRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager, 
            IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, monitorFactory, identityManager, instanceProvider)
        {
        } 

        #endregion

        #region Public methods


        #endregion
    }
}
