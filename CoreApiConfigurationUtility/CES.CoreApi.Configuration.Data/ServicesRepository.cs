using System.Collections.Generic;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Configuration.Model.DomainEntities;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Configuration.Data
{
    public class ServicesRepository : BaseGenericRepository, IServicesRepository
    {
        public ServicesRepository(ICacheProvider cacheProvider, ILogMonitorFactory logMonitorFactory)
            : base(cacheProvider, logMonitorFactory, ConnectionStrings.Main)
        {
        }

        public IEnumerable<Service> GetList()
        {
            var request = new DatabaseRequest<Service>
            {
                ProcedureName = "coreapi_sp_GetServiceList",
                IsCacheable = true,
                Shaper = reader => new Service(
                    reader.ReadValue<int>("ServiceId"),
                    reader.ReadValue<string>("Name"))
            };
            
            return GetList(request);
        }

    }
}
