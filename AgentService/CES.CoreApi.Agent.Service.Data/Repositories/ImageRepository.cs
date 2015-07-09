using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Agent.Service.Data.Repositories
{
    public class ImageRepository: BaseGenericRepository, IImageRepository
    {
        #region Core

        public ImageRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager)
            : base(cacheProvider, monitorFactory, identityManager, DatabaseType.Image)
        {
        } 

        #endregion

        #region Public methods


        #endregion
    }
}
