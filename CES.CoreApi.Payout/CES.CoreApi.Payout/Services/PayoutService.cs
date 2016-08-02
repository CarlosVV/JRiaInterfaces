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
		private static readonly Lazy<PayoutService> @this = new Lazy<PayoutService>(() => new PayoutService(new PayoutRepository()));
		
		private PayoutService(PayoutRepository repository)
		{
			this.repository = repository;
		}

		public static PayoutOrderResponse GetPayoutOrderInfo(PayoutOrderRequest request)
		{
			Regex regex = new Regex(AppSettings.GoldenCrownPinRegex);
			if (regex.Match(request.OrderPin).Success)
			{
				GoldenCrownProvider provider = new GoldenCrownProvider();
				var transaction=  provider.GetPayoutOrderInfo(request);
				transaction.Transaction.PayoutCurrency = GetCurrencyByCode(transaction.Transaction.PayoutCurrency);			
				return transaction;
			}

			var result = @this.Value.repository.GetTransactionInfo(request);		
			return result;
		}

		private static void SetExternalTransactionInfo(PayoutOrderRequest request,PayoutOrderResponse transaction)
		{
			var transactionInfo = @this.Value.repository.GetExternalTransactionInfo(request, transaction);
			transaction.Fields = transactionInfo.Fields;
			transaction.CustomerServiceMessages = transactionInfo.CustomerServiceMessages;
		}
		private static string GetCurrencyByCode(string code)
		{	
			IsoCurrencyRepository repository = new IsoCurrencyRepository();
			var currenies = repository.GetCurrencies();
			var query = from c in currenies where c.IsoCode == code select c.Symbol;
			return query.FirstOrDefault();
		}

		
	}
}