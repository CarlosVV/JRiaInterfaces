using System;

namespace CES.CoreApi.Payout.Models
{
	//[DataContract]
	public class TransactionInfo
	{
		//[DataMember(Name = "OrderID")]
		public int OrderId { get; set; }
		//[DataMember(Name = "TransferDate")]
		public DateTime OrderDate { get; set; }
		//[DataMember(Name = "TransferStatus")]
		public string OrderStatus { get; set; }
		public decimal PayoutAmount { get; set; }
		public string PayoutCurrency { get; set; }
	
		public string CustomerNameFirst { get; set; }
		public string CustomerNameMid { get; set; }
		public string CustomerNameLast1 { get; set; }
		public string CustomerNameLast2 { get; set; }
		public string CustAddress { get; set; }
		public string CustCity { get; set; }
		public string CustState { get; set; }
		//[DataMember(Name = "CountryFrom")]
		public string CustCountry { get; set; }
		public string CustomerTelNo { get; set; }
		public string CustPostalCode { get; set; }


		public string BeneficiaryNameFirst { get; set; }
		public string BeneficiaryNameMid { get; set; }
		public string BeneficiaryNameLast1 { get; set; }

		public string BeneficiaryNameLast2 { get; set; }
		public string BenAddress { get; set; }

		public string BenCity { get; set; }
		public string BenState { get; set; }

		//[DataMember(Name = "CountryTo")]
		public string BenCountry { get; set; }

		public string BenCountryFullName { get; set; }
		public string BenZip { get; set; }


		public string BenTelNo { get; set; }
		public int BenID { get; set; }

		//[DataMember(Name = "OrderPIN")]
		public string PIN { get; set; }
		//[DataMember]
		public int PASeqID { get; set; }

		//[DataMember]
		public string PayAgent { get; set; }
		//[DataMember]
		public string PayAgentBranchName { get; set; }

		//[DataMember]
		public string PayAgentBranchNo { get; set; }
		//[DataMember]
		public string DeliveryMethod { get; set; }

		//[DataMember]
		public decimal BeneficiaryTax { get; set; }
		//[DataMember]
		public decimal NetAmount { get; set; }


	}
}