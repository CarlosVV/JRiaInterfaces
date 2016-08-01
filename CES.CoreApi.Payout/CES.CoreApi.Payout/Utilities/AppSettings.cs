using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CES.CoreApi.Payout.Utilities
{
	public class AppSettings
	{
		public static int AppId
		{
			get
			{
				int appId;
				int.TryParse(ConfigurationManager.AppSettings["ApplicationId"], out appId);
				return appId;
			}
		}
		public static int AppObjectId
		{
			get
			{
				int appObjectId;
				int.TryParse(ConfigurationManager.AppSettings["AppObjectId"], out appObjectId);
				return appObjectId;
			}
		}

		public static int RiaProviderId
		{
			get
			{
				int riaProviderId;
				int.TryParse(ConfigurationManager.AppSettings["riaProviderId"], out riaProviderId);
				return riaProviderId;
			}
		}
	}
}