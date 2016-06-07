using System.Configuration;


namespace CES.CoreApi.Security.Tools
{
    public static class ConfigurationTools
    {
        public static string ReadAppSettingsValue(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default(string);

            var configurationValue = ConfigurationManager.AppSettings[name];

			return configurationValue == null
				? default(string)
				: configurationValue.ToString();
        }
		public static int ReadAppSettingsValueAsInt(string name)
		{
			if (string.IsNullOrEmpty(name))
				return default(int);

			var configurationValue = ConfigurationManager.AppSettings[name];

			return configurationValue == null
				? default(int)
				: System.Convert.ToInt32(configurationValue);
		}
	}
}
