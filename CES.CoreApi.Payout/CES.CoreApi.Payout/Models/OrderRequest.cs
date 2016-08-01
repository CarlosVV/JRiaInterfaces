namespace CES.CoreApi.Payout.ViewModels
{
	public class OrderRequest
	{
		public int AgentId { get; set; }
		public int AgentLocId { get; set; }
		public int UserId { get; set; }
		public int UserLoginId { get; set; }
		public string Locale { get; set; }
		public string OrderId { get; set; }
		public string OrderPin { get; set; }
		public string CountryTo { get; set; }
		public string StateTo { get; set; }

	}
}