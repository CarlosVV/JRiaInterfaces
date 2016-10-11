using System.Collections.ObjectModel;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CES.CoreApi.Foundation.Data.Utility;

using CES.CoreApi.Shared.Providers.Helper.Interfaces;
using CES.CoreApi.Shared.Providers.Helper.Model.Public;
using CES.CoreApi.Shared.Providers.Helper.Model.Public.Enums;
using CES.CoreApi.Shared.Providers.Helper.Constants;
using CES.CoreApi.Data.Base;
using CES.CoreApi.Data.Models;
using CES.CoreApi.Data.Enumerations;

namespace CES.CoreApi.Shared.Providers.Helper.Data
{
	public class ProviderRepository: BaseGenericRepository, IProviderRepository
    {
  

        #region Public Methods
        public async Task<IEnumerable<ProviderModel>> GetProvidersByTypeAsync(ProviderType type)
        {
            var request = new DatabaseRequest<ProviderModel>
            {
                ProcedureName = StoreProcedureConstants.GetProvidersByTypeID,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@providerTypeID",type),
                  
                },
                Shaper = reader => GetProvider(reader, type)
            };
            return await Task.Run(() => GetList(request));
        }
        public IEnumerable<ProviderModel> GetProvidersByType(ProviderType type)
        {
            var request = new DatabaseRequest<ProviderModel>
            {
                ProcedureName = StoreProcedureConstants.GetProvidersByTypeID,
                IsCacheable = false,
                DatabaseType = DatabaseType.Main,
                Parameters = new Collection<SqlParameter>
                {
                    new SqlParameter("@providerTypeID",type),

                },
                Shaper = reader => GetProvider(reader, type)
            };
            return GetList(request);
        }

        #endregion

        #region Private Methods

        private static List<ProviderModel> GetProviders(IDataReader reader, ProviderType type)
        {           
            var providers = new List<ProviderModel>();
            while(reader.Read())
            {
                providers.Add(GetProvider(reader, type));
            }
            return providers;
        }

        private static ProviderModel  GetProvider(IDataReader reader, ProviderType type)
        {
            var provider = new ProviderModel() {
                ProviderID = reader.ReadValue<int>("fProviderID"),
                ProviderTypeID = (int)type,
                Name = reader.ReadValue<string>("fName"),                
            };

            try
            {
                provider.LoadConfigurationFromJson(reader.ReadValue<string>("fConfiguration"));
            }
            catch
            {

            }
           

            return provider;
        }

        #endregion
    }
}
