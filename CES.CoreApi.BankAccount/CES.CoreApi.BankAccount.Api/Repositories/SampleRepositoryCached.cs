using System.Collections.Generic;
using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Models.DTOs;

namespace CES.CoreApi.BankAccount.Api.Repositories
{
    public class SampleRepositoryCached : SampleRepository
    {
        public override IEnumerable<CurrencyCountry> GetServiceOfferdCurrencies(CountryCurrencyRequest request)
        {

            var key = $"GetServiceOfferdCurrencies_{request.CountryFrom}{request.CountryTo}";

            return
            Caching.Cache.Get(key, () =>
                 base.GetServiceOfferdCurrencies(request)
            );
        }
    }
}