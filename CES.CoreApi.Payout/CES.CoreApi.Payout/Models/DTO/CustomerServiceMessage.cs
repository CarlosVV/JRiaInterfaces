using System;

namespace CES.CoreApi.Payout.Models
{
	public class CustomerServiceMessage
	{
		public int MessageID { get; set; }
		public string Category { get; set; }
		public DateTime MsgTime { get; set; }
		public string EnteredBy { get; set; }
		public string MessageBody { get; set; }
	}
}