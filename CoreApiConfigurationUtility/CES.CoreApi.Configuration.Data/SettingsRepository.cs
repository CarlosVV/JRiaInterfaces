using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Configuration.Model.DomainEntities;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Factories;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Configuration.Data
{
    public class SettingsRepository : BaseGenericRepository, ISettingsRepository
    {
        public SettingsRepository(ICacheProvider cacheProvider, ILogMonitorFactory logMonitorFactory)
            : base(cacheProvider, logMonitorFactory, ConnectionStrings.Main)
        {
        }

        public IEnumerable<Setting> GetList(int serviceApplicationId)
        {
            var request = new DatabaseRequest<Setting>
            {
                ProcedureName = "coreapi_sp_GetServiceSettingList",
                IsCacheable = false,
                Shaper = reader => new Setting(
                    reader.ReadValue<int>("Id"),
                    reader.ReadValue<string>("Name"),
                    reader.ReadValue<string>("Value"),
                    reader.ReadValue<string>("Description")),
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@serviceAppId", serviceApplicationId)
                }
            };
            return GetList(request);
        }

        public void Update(Setting setting)
        {
            var request = new DatabaseRequest<Setting>
            {
                ProcedureName = "coreapi_sp_UpdateServiceSetting",
                IncludeConventionParameters = true,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@settingId", setting.Id),
                    new SqlParameter("@value", setting.Value),
                    new SqlParameter("@description", setting.Description)
                }
            };
            Update(request);
        }
    }
}
