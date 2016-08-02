using CES.CoreApi.Payout.ViewModels;
using CES.CoreApi.Payout.Repositories;
using System;
using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.Providers;
using System.Text.RegularExpressions;
using CES.CoreApi.Payout.Utilities;
using System.Linq;

namespace CES.CoreApi.Payout.Services
{

	public class PayoutService
	{
		private readonly PayoutRepository repository;
		private static readonly Lazy<PayoutService> @this 
				= new Lazy<PayoutService>(() => new PayoutService(new PayoutRepository()));
		
		private PayoutService(PayoutRepository repository)
		{
			this.repository = repository;
		}
		/// <summary>
		/// Handle request and route it to Golden Crown web service  or Ria repository 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static PayoutOrderResponse GetTransactionInfo(PayoutOrderRequest request)
		{
			Regex regex = new Regex(AppSettings.GoldenCrownPinRegex);
			if (regex.Match(request.OrderPin).Success)
			{
				var provider = new GoldenCrownCachedProvider();
				var serviceResponse=  provider.GetTransactionInfo(request);
				if (serviceResponse.Transaction == null)
					return serviceResponse;
				serviceResponse.Transaction.PayoutCurrency = GetCurrencyByCode(serviceResponse.Transaction.PayoutCurrency);
				SetExternalTransactionInfo(request, serviceResponse);
				return serviceResponse;
			}

			var result = @this.Value.repository.GetTransactionInfo(request);		
			return result;
		}
		/// <summary>
		/// To get beneficiary field requirment and customer message 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="response"></param>
		private static void SetExternalTransactionInfo(PayoutOrderRequest request,PayoutOrderResponse response)
		{
			var transactionInfo = @this.Value.repository.GetExternalTransactionInfo(request, response);
			response.Fields = transactionInfo.Fields;
			response.CustomerServiceMessages = transactionInfo.CustomerServiceMessages;
		}
		/// <summary>
		/// Golden Crown return currency in ISO code. This will get currency symbol 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private static string GetCurrencyByCode(string code)
		{
			var cache = new IsoCurrencyCachedRepository();
			var currenies = cache.GetCurrencies();
			var query = from c in currenies where c.IsoCode == code select c.Symbol;
			return query.FirstOrDefault();
		}

		
	}
}