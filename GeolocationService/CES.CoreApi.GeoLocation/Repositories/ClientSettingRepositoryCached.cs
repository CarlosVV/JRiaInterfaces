using CES.Caching;

namespace CES.CoreApi.GeoLocation.Repositories
{
	class ClientSettingRepositoryCached : ClientSettingRepository
	{
		public override string GetClientSetting(int applicationId)
		{
			//return base.GetClientSetting(applicationId); 
			var rules = Cache.Get("GetClientSetting_CoreApiCached",
			  () => {
				  return base.GetClientSetting(applicationId);
			  });

			return rules;
		}
	}
}
