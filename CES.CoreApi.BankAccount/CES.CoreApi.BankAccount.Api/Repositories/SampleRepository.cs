using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Models.DTOs;
using CES.Data.Sql;
using System;
using System.Collections.Generic;

namespace CES.CoreApi.BankAccount.Api.Repositories
{
    /// <summary>
    ///Repository class is  interact with database.
    ///Use this to fetch, to update, to delete or to insert data
    ///Please don’t write any business logic in this class , no validation also.
    ///Don’t wrap the call in the try… catch 
    /// </summary>
    public class SampleRepository
    {
        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();
        public virtual IEnumerable<CurrencyCountry> GetServiceOfferdCurrencies(CountryCurrencyRequest request)
        {

            using (var sql = _sqlMapper.CreateQuery(DatabaseName.ReadOnlyTransactional, "sales_sp_Services_Offered_Currencies_List"))
            {
                sql.AddParam("@fServiceID", 111);
                sql.AddParam("@fDate", DateTime.UtcNow);
                sql.AddParam("@fCountryFrom", request.CountryFrom);
                sql.AddParam("@fCountryTo", request.CountryTo);

                var many = sql.Query<CurrencyCountry>();
                return many;
            }
        }


    }
}