using System.Collections.Generic;
using CES.CoreApi.WebApi.Models;
using CES.CoreApi.WebApi.Models.DTOs;

namespace CES.CoreApi.WebApi.Repositories
{
	public class SampleRepositoryCached: SampleRepository
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