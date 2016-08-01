using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Payout.Models
{
	public class CustomerServiceMessage
	{
		public int MessageId { get; set; }
		public string Category { get; set; }

		public DateTime MsgTime { get; set; }
		public string EnteredBy { get; set; }
		public string MessageBody { get; set; }
	}
}