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
			if (string.IsNullOrEmpty(pin))
				return null;

			var result = repositor.GetConfigurations();
			string pinValue = AdjustPin(pin);
			foreach (var item in result)
			{
				foreach (var setting in item.Settings)
				{
					if(setting.Key.Equals("PinRegExp",System.StringComparison.OrdinalIgnoreCase))
					{
						Regex rgx = new Regex(setting.Value.ToString(), RegexOptions.IgnoreCase);
						if (rgx.Match(pinValue).Success)
							return new {  item.ProviderId, item.ProviderName, Pin=pin, SuggestedPin = pinValue, VerificationLevel = "Regex" };
					}
				}
			}

			return new { ProviderId = 5002, ProviderName = "Ria", Pin = pin, SuggestedPin = pinValue, VerificationLevel = "Default"};
			}

		private string AdjustPin(string value)
		{
			if (value.StartsWith("00"))
				return value;
			Regex rgx = new Regex("^[A-Za-z][A-Za-z0-9]*(?:_[A-Za-z0-9]+)*$", RegexOptions.IgnoreCase);
			if (rgx.Match(value).Success)
				return value;

			rgx = new Regex("^[0-9]{9}$", RegexOptions.IgnoreCase);
			if (rgx.Match(value).Success)
				return $"00{value}";

			rgx = new Regex("^0[0-9]{9}$", RegexOptions.IgnoreCase);
			if (rgx.Match(value).Success)
				return $"0{value}";


			return value;
		}
	}
}
