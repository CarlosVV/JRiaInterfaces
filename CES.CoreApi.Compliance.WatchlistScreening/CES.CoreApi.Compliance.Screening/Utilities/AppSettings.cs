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
              
        public static string PortalURLCustomer
        {
            get
            {
                return ConfigurationManager.AppSettings["portalUrlCustomer"];
            }
        }
        public static string PortalURLBeneficiary
        {
            get
            {
                return ConfigurationManager.AppSettings["portalUrlBeneficiary"];
            }
        }

        public static string PortalURLOnBehalf
        {
            get
            {
                return ConfigurationManager.AppSettings["portalUrlOnBehalf"];
            }
        }

        public static string DefaultRuleCustomer
        {
            get
            {
                return ConfigurationManager.AppSettings["defaultRuleCustomer"];
            }
        }

        public static string DefaultRuleBeneficiary
        {
            get
            {
                return ConfigurationManager.AppSettings["defaultRuleBeneficiary"];
            }
        }

        public static string DefaultRuleOnBehalf
        {
            get
            {
                return ConfigurationManager.AppSettings["defaultRuleOnBehalf"];
            }
        }

        public static string MailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["mailFrom"];
            }
        }
        public static string MailTo
        {
            get
            {
                return ConfigurationManager.AppSettings["mailTo"];
            }
        }

        public static bool UseDefaultRules
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["useDefaultRules"]);
            }
        }
        


    }
}