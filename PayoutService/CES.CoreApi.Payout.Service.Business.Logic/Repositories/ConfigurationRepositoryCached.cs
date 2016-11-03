using System.Collections.Generic;
using CES.CoreApi.Payout.Service.Business.Logic.Models;
using CES.Caching;

namespace CES.CoreApi.Payout.Service.Business.Logic.Repositories
{
	public class ConfigurationRepositoryCached : ConfigurationRepository
	{

		public override List<ServiceConfiguration> GetConfigurations()
		{	
			var configs = Cache.Get("GetConfigurations_CoreApi_Payout_Cached",
			  () => {
				  return base.GetConfigurations();
			  });

			return configs;

		}
	}
}
