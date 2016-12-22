

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
		public static string GoogleClientId
		{
			get
			{

				return ConfigurationManager.AppSettings["GoogleClientId"];

			}

		}
		public static string GooglePrivateCryptoKey
		{
			get
			{
				return ConfigurationManager.AppSettings["GooglePrivateCryptoKey"];
			}
		}
		public static bool GoogleUseKeyForAutoComplete
		{
			get
			{
				bool value;
				bool.TryParse(ConfigurationManager.AppSettings["GoogleUseKeyForAutoComplete"], out value);
				return value;
			}
		}
		public static bool GoogleUseKeyForAddressValidate
		{
			get
			{
				bool value;
				bool.TryParse(ConfigurationManager.AppSettings["GoogleUseKeyForAddressValidate"], out value);
				return value;
			}
		}
	}
}
