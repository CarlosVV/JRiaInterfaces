using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.Utilities;
using CES.Data.Sql;
using System;
using System.Collections.Generic;
using CES.CoreApi.Payout.ViewModels;

namespace CES.CoreApi.Payout.Repositories
{

	internal class PayoutRepository
	{
		private SqlMapper _sqlMapper;
		private DataProvider _dataProvider;
		public PayoutRepository()
		{
			_dataProvider = new DataProvider { ProviderName = "Ria", ProviderId = AppSettings.RiaProviderId };
			_sqlMapper = DatabaseName.CreateSqlMapper();
		}

		public PayoutOrderResponse GetTransactionInfo(PayoutOrderRequest payoutOrderInfo)
		{
			var response = null as PayoutQueryResponse;
			var transaction = null as Transaction;
			var messages = null as IEnumerable<CustomerServiceMessage>;
			var fields = null as IEnumerable<PayoutField>;
			using (var sql = _sqlMapper.CreateQueryAgain(DatabaseName.Main, "mt_sp_Payout_OrderInfo_Get"))
			{
				sql.AddParam("@lAppID", AppSettings.AppId);
				sql.AddParam("@lAppObjectID", AppSettings.AppObjectId);
				sql.AddParam("@lAgentID", payoutOrderInfo.AgentId);
				sql.AddParam("@lAgentLocID", payoutOrderInfo.AgentLocId);
				sql.AddParam("@lUserNameID", payoutOrderInfo.UserLoginId);
				sql.AddParam("@sLocale", payoutOrderInfo.Locale);
				sql.AddParam("@lOrderID", payoutOrderInfo.OrderId);
				sql.AddParam("@OrderRefNo", payoutOrderInfo.OrderPin);
				sql.AddParam("@OrderLookupCode", DBNull.Value);
				sql.AddParam("@AgentCountry", payoutOrderInfo.CountryTo);
				sql.AddParam("@AgentState", payoutOrderInfo.StateTo);

				sql.QueryAgain(reader =>
				{

					response = reader.QueryOne<PayoutQueryResponse>();
					transaction = reader.QueryOne<Transaction>();
					messages = reader.Query<CustomerServiceMessage>();
					fields = reader.Query<PayoutField>();
				});								
			}			
		
			return new PayoutOrderResponse
			{				
				Response = response,
				Transaction = transaction,
				CustomerServiceMessages = messages,
				Fields = fields,
				ProviderInfo = _dataProvider
			}; 
		}

		public PayoutOrderResponse GetExternalTransactionInfo(PayoutOrderRequest request, PayoutOrderResponse info)
		{
			var response = null as PayoutQueryResponse;
			var transaction = null as Transaction;
			var messages = null as IEnumerable<CustomerServiceMessage>;
			var fields = null as IEnumerable<PayoutField>;
			using (var sql = _sqlMapper.CreateQueryAgain(DatabaseName.Main, "mt_sp_Payout_OrderInfo_Get_External"))
			{
				sql.AddParam("@lAppID", AppSettings.AppId);
				sql.AddParam("@lAppObjectID", AppSettings.AppObjectId);
				sql.AddParam("@lAgentID", request.AgentId);
				sql.AddParam("@lAgentLocID", request.AgentLocId);
				sql.AddParam("@lUserNameID", request.UserLoginId);
				sql.AddParam("@sLocale", request.Locale);
				
				sql.AddParam("@TellerDrawerInstanceID", -1);
				//
				sql.AddParam("@AgentCountry", request.CountryTo);
				sql.AddParam("@lAgentCountryID", -1);
				//
				sql.AddParam("@AgentState", request.StateTo);
				sql.AddParam("@Payout_LocalTime", request.LocalTime);
				sql.AddParam("@SendAgentID", 5002);
				sql.AddParam("@SendAgentLocID", -1);
				sql.AddParam("@SendAgentBranchNo", "");
				sql.AddParam("@SendAgentCountry", info.Transaction.CustCountry);
				sql.AddParam("@SendAgentState", "");
				sql.AddParam("@SendAgentRequiredFields", "");
				//
				sql.AddParam("@lOrderID", request.OrderId);
				sql.AddParam("@OrderLookupCode", DBNull.Value);
				sql.AddParam("@PIN", request.OrderPin);

				//
				sql.AddParam("@OrderNo", "");
				sql.AddParam("@OrderDate", info.Transaction.OrderDate);
				sql.AddParam("@OrderStatus", info.Transaction.OrderStatus);
				sql.AddParam("@OrderStatusTime", "");
				sql.AddParam("@bAvailableForPayout", false);
				sql.AddParam("@PayoutCurrency", info.Transaction.PayoutCurrency);

				sql.AddParam("@PayoutAmount", info.Transaction.PayoutAmount);
				sql.AddParam("@PayoutCountry", request.CountryTo);
				sql.AddParam("@SendCurrency", info.Transaction.PayoutCurrency);
				sql.AddParam("@SendAmount", info.Transaction.PayoutAmount);
				sql.AddParam("@SendCharge", 0.01m);

				sql.AddParam("@SenderInternalID", 0);
				sql.AddParam("@SenderExternalNo","0");
				sql.AddParam("@SenderNameFirst", info.Transaction.CustomerNameFirst);
				sql.AddParam("@SenderNameMid", info.Transaction.CustomerNameMid);

				//sql.AddParam("@OrderRefNo", request.OrderPin);
				sql.AddParam("@SenderNameLast1", info.Transaction.CustomerNameLast1);
				sql.AddParam("@SenderNameLast2", info.Transaction.CustomerNameLast2);
				sql.AddParam("@SenderTelNo", info.Transaction.CustomerTelNo);
				sql.AddParam("@SenderAddress", info.Transaction.CustAddress);
				sql.AddParam("@SenderCity", info.Transaction.CustCity);

				sql.AddParam("@SenderState", info.Transaction.CustState);
				sql.AddParam("@SenderPostalCode", info.Transaction.CustPostalCode);
				sql.AddParam("@SenderCountry", info.Transaction.CustCountry);
				sql.AddParam("@BenInternalNameID", 0);
				sql.AddParam("@BenInternalID", 0);

				sql.AddParam("@BenExternalNo", "0");
				sql.AddParam("@BenNameFirst", info.Transaction.BeneficiaryNameFirst);
				sql.AddParam("@BenNameMid", info.Transaction.BeneficiaryNameMid);
				sql.AddParam("@BenNameLast1", info.Transaction.BeneficiaryNameLast1);
				sql.AddParam("@BenNameLast2", info.Transaction.BeneficiaryNameLast2);

				sql.AddParam("@BenTelNo", "0");
				sql.AddParam("@BenAddress", info.Transaction.BenAddress);
				sql.AddParam("@BenCity", info.Transaction.BenCity);
				sql.AddParam("@BenState", info.Transaction.BenState);
				sql.AddParam("@BenZip", info.Transaction.BenZip);
				sql.AddParam("@BenCountry", info.Transaction.BenCountry);

				sql.QueryAgain(reader =>
				{

					response = reader.QueryOne<PayoutQueryResponse>();
					transaction = reader.QueryOne<Transaction>();
					messages = reader.Query<CustomerServiceMessage>();
					fields = reader.Query<PayoutField>();
				});
			}

			return new PayoutOrderResponse
			{
				Response = response,
				Transaction = transaction,
				CustomerServiceMessages = messages,
				Fields = fields,
				ProviderInfo = _dataProvider
			};
		}

	}
}