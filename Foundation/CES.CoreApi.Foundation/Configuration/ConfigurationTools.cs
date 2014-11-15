using System.Configuration;
using CES.CoreApi.Common.Tools;

namespace CES.CoreApi.Foundation.Configuration
{
    public static class ConfigurationTools
    {
        public static T ReadAppSettingsValue<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default(T);

            var configurationValue = ConfigurationManager.AppSettings[name];

            return configurationValue == null
                ? default(T)
                : configurationValue.ConvertValue<T>();
        }
    }
}
