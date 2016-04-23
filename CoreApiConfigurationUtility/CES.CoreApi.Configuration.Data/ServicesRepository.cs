using System.Collections.Generic;

using CES.CoreApi.Configuration.Model.DomainEntities;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Data.Base;
using CES.CoreApi.Data.Models;
using CES.CoreApi.Data.Enumerations;

namespace CES.CoreApi.Configuration.Data
{
	public class ServicesRepository : BaseGenericRepository, IServicesRepository
    {
   
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
