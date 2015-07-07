using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Data.Base;
using CES.CoreApi.Foundation.Data.Utility;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Settings.Service.Business.Contract.Interfaces;
using CES.CoreApi.Settings.Service.Business.Contract.Models;

namespace CES.CoreApi.Settings.Service.Data.Repositories
{
    public class CountryRepository : BaseGenericRepository, ICountryRepository
    {
        #region Core

        public CountryRepository(ICacheProvider cacheProvider, ILogMonitorFactory monitorFactory, IIdentityManager identityManager) :
            base(cacheProvider, monitorFactory, identityManager, DatabaseType.ReadOnly)
        {
        }

        #endregion

        #region Public methods

        public IEnumerable<CountryModel> GetAll(int languageId, string abbreviationList)
        {
            var request = new DatabaseRequest<CountryModel>
            {
                ProcedureName = "ol_sp_systblRegCountries_GetByAbbrevCsvAndLanguageID",
                IsCacheable = true,
                Parameters = new Collection<SqlParameter>()
                    .Add("@flanguageID", languageId)
                    .Add("@sCountryAbbrevCsv", abbreviationList),
                Shaper = reader => GetCountryDetails(reader)
            };

            return GetList(request);
        }

        public IEnumerable<CountryModel> GetAll(string culture, string abbreviationList)
        {
            var request = new DatabaseRequest<CountryModel>
            {
                ProcedureName = "ol_sp_systblRegCountries_GetByAbbrevCsvAndCulture",
                IsCacheable = true,
                Parameters = new Collection<SqlParameter>()
                    .Add("@sCulture", culture)
                    .Add("@sCountryAbbrevCsv", abbreviationList),
                Shaper = reader => GetCountryDetails(reader)
            };

            return GetList(request);
        }

        public DatabasePingModel Ping()
        {
            return PingDatabase();
        }

        #endregion

        #region Private methods

        private static CountryModel GetCountryDetails(IDataReader reader)
        {
            return new CountryModel
            {
                Id = reader.ReadValue<int>("fCountryID"),
                Name = reader.ReadValue<string>("fCountry"),
                Abbreviation = reader.ReadValue<string>("fAbbrev"),
                Code = reader.ReadValue<string>("fCode"),
                Currency = reader.ReadValue<string>("fCountryCurrency"),
                IsoCode = reader.ReadValue<string>("fISOCode"),
                IsoCodeAlpha3 = reader.ReadValue<string>("fISOCode_Alpha3"),
                Note = reader.ReadValue<string>("fNote"),
                Order = reader.ReadValue<int>("fOrder"),
                UseCityList = reader.ReadValue<bool>("fUseCityList")
            };
        }

        #endregion
    }
}
