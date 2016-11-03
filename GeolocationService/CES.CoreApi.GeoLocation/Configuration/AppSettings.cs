

using System.Configuration;

namespace CES.CoreApi.GeoLocation.Configuration
{
	public class AppSettings
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
		public static string GoogleMapGeocodeUrl
		{
			get
			{

				return ConfigurationManager.AppSettings["GoogleMapGeocodeUrl"];
				
			}

		}
	}
}
