using System.Collections.Generic;
using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Models.DTOs;

namespace CES.CoreApi.Receipt_Main.Repositories
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