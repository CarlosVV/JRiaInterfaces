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
		private  PayoutRepository _repository =null;
		private GoldenCrownCachedProvider _provider =null;
		//private static readonly Lazy<PayoutService> @this 
		//		= new Lazy<PayoutService>(() => new PayoutService(new PayoutRepository()));

		public PayoutService()
		{
			_repository =new PayoutRepository();
		}
		/// <summary>
		/// Handle request and route it to Golden Crown web service  or Ria repository 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public  PayoutOrderResponse GetTransactionInfo(PayoutOrderRequest request)
		{
			Regex regex = new Regex(AppSettings.GoldenCrownPinRegex);
			if (regex.Match(request.OrderPin).Success)
			{
				_provider = new GoldenCrownCachedProvider();
				var serviceResponse= _provider.GetTransactionInfo(request);
				if (serviceResponse.Transaction == null)
					return serviceResponse;
				serviceResponse.Transaction.PayoutCurrency = GetCurrencyByCode(serviceResponse.Transaction.PayoutCurrency);
				SetExternalTransactionInfo(request, serviceResponse);
				return serviceResponse;
			}

			var result = _repository.GetTransactionInfo(request);		
			return result;
		}
		/// <summary>
		/// To get beneficiary field requirment and customer message 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="response"></param>
		private  void SetExternalTransactionInfo(PayoutOrderRequest request,PayoutOrderResponse response)
		{
			var transactionInfo = _repository.GetExternalTransactionInfo(request, response);
			response.Fields = transactionInfo.Fields;
			response.CustomerServiceMessages = transactionInfo.CustomerServiceMessages;
		}
		/// <summary>
		/// Golden Crown return currency in ISO code. This will get currency symbol 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		private  string GetCurrencyByCode(string code)
		{
			var cache = new IsoCurrencyCachedRepository();
			var currenies = cache.GetCurrencies();
			var query = from c in currenies where c.IsoCode == code select c.Symbol;
			return query.FirstOrDefault();
		}

		
	}
}