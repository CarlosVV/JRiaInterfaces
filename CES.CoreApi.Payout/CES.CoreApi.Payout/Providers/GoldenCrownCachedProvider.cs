using CES.Caching;
using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.ViewModels;

namespace CES.CoreApi.Payout.Providers
{
	public class GoldenCrownCachedProvider :GoldenCrownProvider
	{
		public override PayoutOrderResponse GetTransactionInfo(PayoutOrderRequest request)
		{
			var currencies = Cache.Get<PayoutOrderResponse>(string.Format("GoldenCrown_OrderPin_{0}",
					request.OrderPin), null);
			if (currencies == null)
				return base.GetTransactionInfo(request);
				
			return currencies;
		}
	}
}