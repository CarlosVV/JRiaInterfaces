using System.Collections.Generic;

namespace CES.CoreApi.Payout.Service.Business.Logic.Models
{
	public class ApiSetting
	{
		public string Key { get; set; }
		public object Value { get; set; }
	}
	public class ServiceConfiguration
	{
		public int ProviderId { get; set; }
		public string ProviderName { get; set; }
		public IList<ApiSetting> Settings { get; set; }
	}
}
