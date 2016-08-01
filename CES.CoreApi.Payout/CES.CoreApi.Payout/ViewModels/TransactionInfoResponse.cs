using CES.CoreApi.Payout.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CES.CoreApi.Payout.ViewModels
{
	[DataContract]
	public class TransactionInfoResponse
	{
		[DataMember(Name = "PersistenceID")]
		public int PersistenceId { get; set; }

		[DataMember(Name = "OrderID")]
		public int OrderId { get; set; }
		[DataMember(Name = "TransferDate")]
		public DateTime OrderDate { get; set; }
		[DataMember(Name = "TransferStatus")]
		public string OrderStatus { get; set; }
		public decimal PayoutAmount { get; set; }

		public string PayoutCurrency { get; set; }
		[DataMember(Name = "PayoutAmount")]
		public Amount @Amount
		{
			get { return new Amount { Currency = this.PayoutCurrency, Value = this.PayoutAmount }; }
		}

		[DataMember(Name = "CountryFrom")]
		public string CustCountry { get; set; }
		[DataMember(Name = "CountryTo")]
		public string BenCountry { get; set; }
		[DataMember(Name = "OrderPIN")]
		public string PIN { get; set; }
		[DataMember]
		public int PASeqID { get; set; }

		[DataMember]
		public string PayAgent { get; set; }
		[DataMember]
		public string PayAgentBranchName { get; set; }

		[DataMember]
		public string PayAgentBranchNo { get; set; }
		[DataMember]
		public string DeliveryMethod { get; set; }

		[DataMember]
		public decimal BeneficiaryTax { get; set; }
		[DataMember]
		public decimal NetAmount { get; set; }

		

		[DataMember]
		public PayoutQueryResponse Response
		{
			get;set;
		}
		[DataMember]
		public IEnumerable<CustomerServiceMessage> CustomerServiceMessages { get; set; }
		[DataMember(Name = "PayoutRequiredFields")]
		public IEnumerable<PayoutField> Fields
		{
			get; set;
		}
		[DataMember]
		public Sender SenderInfo { get; set; }
		[DataMember]
		public Beneficiary BeneficiaryInfo { get; set; }
		[DataMember]
		public DataProvider ProviderInfo { get; set; }
		[DataMember]
		public DateTime ResponseTime { get
			{
				return DateTime.UtcNow;
			}
		}
		[DataMember]
		public string Message { get; set; }

	}
}