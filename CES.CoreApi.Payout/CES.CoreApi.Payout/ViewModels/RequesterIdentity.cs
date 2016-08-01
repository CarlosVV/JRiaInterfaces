using System;

namespace CES.CoreApi.Payout.ViewModels
{
	public class RequesterIdentity
	{
		public int AppObjectId { get; set; }
		public int AgentId { get; set; }
		public int AgentLocId { get; set; }
		public int UserId { get; set; }
		public int UserLoginId { get; set; }
		public string Locale { get; set; }
		public string TerminalId { get; set; }
		public string TerminalUserId { get; set; }
		public DateTime LocalTime { get; set; }
		public DateTime UtcTime { get; set; }
		public int Timezone { get; set; }
		public int TimezoneId { get; set; }
		public string Version { get; set; }
		public string AgentCountry { get; set; }
		public string AgentState { get; set; }

		
	}
}

