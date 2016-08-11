using CES.Caching;
using CES.CoreApi.Payout.Models.DTO;
using System;
using System.Collections.Generic;

namespace CES.CoreApi.Payout.Repositories
{
	public class IsoCurrencyRepositoryCached: IsoCurrencyRepository
	{
		public override IEnumerable<Currency> GetCurrencies()
		{
			var currencies = Cache.Get("GetAllCurrencies_Cached",
				() => {
					return base.GetCurrencies();
				}, new TimeSpan(12, 0, 0));

			return currencies;
		}
	}
}