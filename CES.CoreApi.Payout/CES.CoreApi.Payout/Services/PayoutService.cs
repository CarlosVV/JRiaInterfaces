using CES.CoreApi.Payout.ViewModels;
using CES.CoreApi.Payout.Repositories;
using System;
using System.Collections.Generic;
using CES.CoreApi.Payout.Models;

namespace CES.CoreApi.Payout.Services
{
	/// <summary>
	/// Sample service class please replace it or remove it.
	///	Use Service class to implement:
	///  1)	Business logic
	///  2)	Validation 
	///  3)	Caching
	/// </summary>
	public class PayoutService
	{
		private readonly PayoutRepository repository;
		private static readonly Lazy<PayoutService> @this = new Lazy<PayoutService>(() => new PayoutService(new PayoutRepository()));

		private PayoutService(PayoutRepository repository)
		{
			this.repository = repository;
		}

		public static PayoutOrderInfo GetPayoutOrderInfo(OrderRequest request)
		{
			
			var result = @this.Value.repository.GetPayoutOrderInfo(request);
		
			return result;
		}
	}
}