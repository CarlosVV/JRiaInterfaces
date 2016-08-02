using AutoMapper;
using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.Utilities;
using CES.CoreApi.Payout.ViewModels;
using System.Diagnostics;

namespace CES.CoreApi.Payout.Providers
{
	public class GoldenCrownProvider
	{
		private DataProvider _dataProvider;
		public GoldenCrownProvider()
		{
			_dataProvider = new DataProvider { ProviderName = "Golden Crown",
				ProviderId = AppSettings.GoldenCrownProviderId
			};
		}

		public PayoutOrderResponse GetPayoutOrderInfo(PayoutOrderRequest request)
		{
			var response = new PayoutOrderResponse { ProviderInfo = _dataProvider };
		
			ReqTxList transactionRequest = new ReqTxList();		
			transactionRequest.Filter = new Filter
			{
				OID = request.OrderPin
			};					
			transactionRequest.ExtensionInfo =  new ExtensionInfo
			{
				InterfaceVersionSpecified = true,
				InterfaceVersion = AppSettings.GoldenCrownInterfaceVersion
			}; 
			transactionRequest.AgentID = request.AgentLocId.ToString();

			var providerResponse = null as Tx[];
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			using (ServicePortClient client = GoldenCrownUtility.CreateServicePortClient())
			{
			
				providerResponse = client.TxList(transactionRequest);
			
			}
			stopwatch.Stop();
			Logging.Log.Info(string.Format("Provider : GoldenCrown , Service Method TxList, Duration {0} Milliseconds", stopwatch.ElapsedMilliseconds));

			if (providerResponse == null || providerResponse.Length < 1)
				return null;

			Tx tx = providerResponse[0];
			response.Transaction = Mapper.Map<Transaction>(tx);
			response.Transaction.OrderStatus = tx.TransferStatus.ToOrderStatusString();
			var amt = tx.PayData.PayFunds.GetAmount();
			response.Transaction.PayoutAmount = amt.Value;
			response.Transaction.PayoutCurrency = amt.Currency;
			response.Transaction.CustCountry = tx.PayData.FromCountryISO;
			
			var sender = tx.PayData.Item as Person;
			sender.SetSender(response.Transaction);
			var bene = tx.PayData.Item1 as Person;
			bene.SetBeneficiary(response.Transaction);

			return response;
		}

		

	}
}