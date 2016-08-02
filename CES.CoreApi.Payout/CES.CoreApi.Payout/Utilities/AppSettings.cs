using System.Configuration;

namespace CES.CoreApi.Payout.Utilities
{
	public  static class AppSettings
	{
		private static int ToNumber(this string value)
		{
			int num;
			int.TryParse(value, out num);
			return num;
		}
		public static int AppId
		{
			get
			{
				return ConfigurationManager.AppSettings["ApplicationId"].ToNumber();
			}
		}
		public static int AppObjectId
		{
			get
			{			
				return ConfigurationManager.AppSettings["AppObjectId"].ToNumber();				
			}
		}

		public static int RiaProviderId
		{
			get
			{			
				return ConfigurationManager.AppSettings["riaProviderId"].ToNumber();				
			}
		}
		public static int GoldenCrownProviderId
		{
			get
			{
				return ConfigurationManager.AppSettings["goldenCrownProviderId"].ToNumber();

			}
		}
		public static float GoldenCrownInterfaceVersion
		{
			get
			{
				return ConfigurationManager.AppSettings["goldenCrownInterfaceVersion"].ToNumber();			
			}
		}

		public static string GoldenCrownServiceUrl
		{
			get
			{

				return ConfigurationManager.AppSettings["goldenCrownServiceUrl"];
		
			}
		}

		public static string GoldenCrownClientCertSubject
		{
			get
			{
			
				return ConfigurationManager.AppSettings["goldenCrownClientCertSubject"];
				
			}
		}
		public static string GoldenCrownServiceSubject
		{
			get
			{

				return ConfigurationManager.AppSettings["goldenCrownServiceSubject"];
			
			}
		}
		public static string GoldenCrownPinRegex
		{
			get
			{

				return ConfigurationManager.AppSettings["goldenCrownPinRegex"];

			}
		}
		public static string RiaPinRegex
		{
			get
			{

				return ConfigurationManager.AppSettings["riaPinRegexp"];

			}
		}

		
	}
}