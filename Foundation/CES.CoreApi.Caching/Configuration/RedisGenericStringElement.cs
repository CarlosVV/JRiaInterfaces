using System.Configuration;

namespace CES.CoreApi.Caching.Configuration
{
    public class RedisGenericStringElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue = "", IsRequired = false)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }

    }
}
