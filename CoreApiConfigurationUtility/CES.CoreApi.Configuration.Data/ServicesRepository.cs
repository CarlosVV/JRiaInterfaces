using System.Collections.Generic;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Configuration.Model.DomainEntities;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Foundation.Data.Models;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Configuration.Data
{
    public class ServicesRepository : BaseGenericRepository, IServicesRepository
    {
        public ServicesRepository(ICacheProvider cacheProvider, ILogMonitorFactory logMonitorFactory, IIdentityManager identityManager,
             IDatabaseInstanceProvider instanceProvider)
            : base(cacheProvider, logMonitorFactory, identityManager, instanceProvider)
        {
        }

        public IEnumerable<Service> GetList()
        {
            var request = new DatabaseRequest<Service>
            {
                ProcedureName = "coreapi_sp_GetServiceList",
                IsCacheable = true,
                DatabaseType = DatabaseType.Main,
                Shaper = reader => new Service(
                    reader.ReadValue<int>("ServiceId"),
                    reader.ReadValue<string>("Name"))
            };
            
            return GetList(request);
        }

    }
}
