using System;

namespace CES.CoreApi.Payout.Models
{

	public class OrderInfo
	{

		public int OrderId { get; set; }
		public DateTime OrderDate { get; set; }
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
		public string BenCountry { get; set; }

		public string BenCountryFullName { get; set; }
		public string BenZip { get; set; }
		public string BenTelNo { get; set; }
		public int BenID { get; set; }
		public string PIN { get; set; }
		public int PASeqID { get; set; }
		public string PayAgent { get; set; }
		public string PayAgentBranchName { get; set; }
		public string PayAgentBranchNo { get; set; }
		public string DeliveryMethod { get; set; }
		public decimal BeneficiaryTax { get; set; }

		public decimal NetAmount { get; set; }


	}
}