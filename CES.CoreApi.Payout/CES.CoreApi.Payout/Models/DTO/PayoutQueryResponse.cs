using System.Runtime.Serialization;

namespace CES.CoreApi.Payout.Models
{
	[DataContract]
	public class PayoutQueryResponse
	{
		[DataMember]
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		[DataMember]
		public bool AvailableForPayout { get; set; }
		public bool AllowUnusualOrderReporting { get; set; }
		public bool RemainingBalanceWarningMsg { get; set; }
	
	}
}