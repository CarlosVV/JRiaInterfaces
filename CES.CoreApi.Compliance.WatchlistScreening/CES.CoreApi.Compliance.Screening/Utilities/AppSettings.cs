using System.Configuration;

namespace CES.CoreApi.Compliance.Screening.Utilities
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

	}
}