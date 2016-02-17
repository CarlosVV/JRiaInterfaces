using System;
using System.Configuration;

namespace CES.CoreApi.Caching.Configuration
{
    public class RedisDataCacheClientSection : ConfigurationSection
    {
        [ConfigurationProperty("cacheName", DefaultValue = "DefaultName", IsRequired = true)]
        public string CacheName
        {
            get
            {
                return this["cacheName"].ToString();
            }
            set
            {
                this["cacheName"] = value;
            }
        }

        // Create a "cacheLifetime" attribute.
        [ConfigurationProperty("cacheLifetime", DefaultValue = "0.00:10:00", IsRequired = false)]
        public TimeSpan CacheLifetime
        {
            get
            {
                return TimeSpan.Parse(this["cacheLifetime"].ToString());
            }
            set
            {
                this["cacheLifetime"] = value;
            }
        }

        // Create a "hosts" collection.
        [ConfigurationProperty("hosts")]
        public RedisHostElementCollection Hosts
        {
            get
            {
                return ((RedisHostElementCollection)(base["hosts"]));
            }
            set
            {
                this["hosts"] = value;
            }
        }
    }
}
