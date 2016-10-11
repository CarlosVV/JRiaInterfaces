using System.Configuration;

namespace CES.CoreApi.PushNotifications.Utilities
{
	public static class AppSettings
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

		public static PushSharp.Apple.ApnsConfiguration.ApnsServerEnvironment ApnsEnvironment
		{
			get
			{
				return (PushSharp.Apple.ApnsConfiguration.ApnsServerEnvironment)
				  ConfigurationManager.AppSettings["apns_environment"].ToNumber();
			}
		}
		public static string ApnsCertificatePassword
		{
			get { return ConfigurationManager.AppSettings["apns_cert_pwd"]; }
		}
		public static string ApnsCertificatePath
		{
			get
			{
				var path = "~/" + ConfigurationManager.AppSettings["apns_cert_file"];

				return System.Web.Hosting.HostingEnvironment.MapPath(path);
			}
		}

		
	}
}