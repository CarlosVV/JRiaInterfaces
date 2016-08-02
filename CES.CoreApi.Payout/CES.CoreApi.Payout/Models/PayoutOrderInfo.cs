
using System.Collections.Generic;

namespace CES.CoreApi.Payout.Models
{
	public class PayoutOrderInfo
	{
		public int PersistenceId { get; set; }
		public PayoutQueryResponse Response { get; set; }
		public OrderInfo Transaction { get; set; }
		public IEnumerable<CustomerServiceMessage> CustomerServiceMessages { get; set; }
		public IEnumerable<PayoutField> Fields { get; set; }
		public DataProvider ProviderInfo { get; set; }
	}
}