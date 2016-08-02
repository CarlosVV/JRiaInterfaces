using System;

namespace CES.CoreApi.Payout.ViewModels
{
	public class TransactionInfoRequest
	{
		public int PersistenceId { get; set; }
		public string OrderPin { get; set; }
		public string OrderId { get; set; }
		public string CountryTo { get; set; }
		public string StateTo { get; set; }
		public RequesterIdentity RequesterInfo { get; set; }
		public int? AgentId { get { return RequesterInfo?.AgentId; } }
		public int? AgentLocId { get { return RequesterInfo?.AgentLocId; } }
		public int? UserId { get { return RequesterInfo?.UserId; } }
		public int? UserLoginId { get { return RequesterInfo?.UserLoginId; } }
		public string Locale { get { return RequesterInfo?.Locale; } }
		public DateTime? LocalTime { get { return RequesterInfo?.LocalTime; } }
	}
}