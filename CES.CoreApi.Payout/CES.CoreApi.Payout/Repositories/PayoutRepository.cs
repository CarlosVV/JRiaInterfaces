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

		public PayoutOrderInfo GetPayoutOrderInfo(OrderRequest payoutOrderInfo)
		{
			var response = null as PayoutQueryResponse;
			var transaction = null as TransactionInfo;
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
					transaction = reader.QueryOne<TransactionInfo>();
					messages = reader.Query<CustomerServiceMessage>();
					fields = reader.Query<PayoutField>();
				});								
			}			
		
			return new PayoutOrderInfo
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