using System;
using System.Configuration;

namespace CES.CoreApi.Caching.Configuration
{
    public class RedisDataCacheClientSection : ConfigurationSection
    {
        [ConfigurationProperty("cacheName")]
        public RedisGenericStringElement CacheName
        {
            get
            {
                return (RedisGenericStringElement)this["cacheName"];
            }
            set
            {
                this["cacheName"] = value;
            }
        }

        // Create a "cacheLifetime" attribute.
        [ConfigurationProperty("cacheLifetime")]
        public RedisGenericTimestampElement CacheLifetime
        {
            get
            {
                return (RedisGenericTimestampElement)this["cacheLifetime"];
            }
            set
            {
                this["cacheLifetime"] = value;
            }
        }

        [ConfigurationProperty("cacheConfigurationString")]
        public RedisGenericStringElement CacheConfigurationString
        {
            get
            {
                return (RedisGenericStringElement)this["cacheConfigurationString"];
            }
            set
            {
                this["cacheConfigurationString"] = value;
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
