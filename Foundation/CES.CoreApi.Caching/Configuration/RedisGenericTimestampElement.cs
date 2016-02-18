using System;
using System.Configuration;

namespace CES.CoreApi.Caching.Configuration
{
    public class RedisGenericTimestampElement : ConfigurationElement
    {

        [ConfigurationProperty("value", DefaultValue = "0.00:10:00", IsRequired = false)]
        public TimeSpan Value
        {
            get
            {
                return (TimeSpan)(this["value"]);
            }
            set
            {
                this["value"] = value;
            }
        }


    }
}
