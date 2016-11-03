using CES.CoreApi.Payout.Service.Business.Logic.Models;
using CES.CoreApi.Payout.Service.Business.Logic.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CES.CoreApi.Payout.Service.Business.Logic.Services
{
	public class ConfigurationService
	{
		private ConfigurationRepositoryCached repositor;
		
		public ConfigurationService()
		{
			repositor = new ConfigurationRepositoryCached();
		}

		public List<ServiceConfiguration> GetSettings()
		{
			return repositor.GetConfigurations();
		}

		public object GetProvider(string pin)
		{
		
			var result = repositor.GetConfigurations();
			//var apiSettings = (from q in result select new   { q.Settings }) as IList<ApiSetting>;
			foreach (var item in result)
			{
				foreach (var setting in item.Settings)
				{
					if(setting.Key.Equals("PinRegExp",System.StringComparison.OrdinalIgnoreCase))
					{
						Regex rgx = new Regex(setting.Value.ToString(), RegexOptions.IgnoreCase);
						if (rgx.Match(pin).Success)
							return new {  item.ProviderId, item.ProviderName };
					}
				}
			}

			return null;

		}
	}
}
