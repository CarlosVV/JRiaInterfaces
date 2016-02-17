using System.Configuration;

namespace CES.CoreApi.Caching.Configuration
{
    public class RedisHostElement : ConfigurationElement
    {
        [ConfigurationProperty("priority", DefaultValue = "1", IsRequired = false)]
        [IntegerValidator(ExcludeRange = false, MinValue = 1)]
        public int Priority
        {
            get
            {
                return (int)this["priority"];
            }
            set
            {
                this["priority"] = value;
            }
        }

        [ConfigurationProperty("server", DefaultValue = "127.0.0.1", IsRequired = true)]
        public string Server
        {
            get
            {
                return (string)this["server"];
            }
            set
            {
                this["server"] = value;
            }
        }

        [ConfigurationProperty("port", DefaultValue = "6379", IsRequired = true)]
        [IntegerValidator(ExcludeRange = false, MinValue = 1)]
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }
    }
}
