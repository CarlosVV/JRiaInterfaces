

using System.Configuration;

namespace CES.CoreApi.GeoLocation.Configuration
{
	public class Settings
	{
		public static int ApplicationId
		{
			get
			{
				int appId;
				int.TryParse(ConfigurationManager.AppSettings["ApplicationID"], out appId);
				return appId;
			}

		}
	}
}
