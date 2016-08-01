using System.Runtime.Serialization;

namespace CES.CoreApi.Payout.Models
{
	[DataContract]
	public class Amount
	{
		[DataMember(Name ="Amount")]
		public decimal Value { get; set; }
		[DataMember(Name = "CurrencyCode")]
		public string Currency { get; set; }
	}
}