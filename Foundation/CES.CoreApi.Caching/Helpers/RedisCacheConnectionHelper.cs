using System;
using System.Configuration;
using StackExchange.Redis;
using CES.CoreApi.Caching.Configuration;

namespace CES.CoreApi.Caching.Helpers
{
    public static class RedisCacheConnectionHelper
    {
        const string sectionName = "dataCacheRedisClient";
        public static  IDatabase GetDatabase()
        {
            var dataBase = Connection.GetDatabase();
            
            return dataBase;
        }

        public static  RedisDataCacheClientSection GetConfig()
        {
            return GetConfiguration();
        }

       

        public static void ClearCache()
        {
            var config = GetConfiguration();

            var options = new ConfigurationOptions();
            foreach (RedisHostElement host in config.Hosts)
            {
                var server = Connection.GetServer(host.Server, host.Port);
                server.FlushDatabase();
            }
        }
        private static  Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            var config = GetConfiguration();
            var options = ConfigurationOptions.Parse(config.CacheConfigurationString.Value);
            options.ClientName = config.CacheName.Value;      

            return ConnectionMultiplexer.Connect(options); ;
        });

        private static  ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        private static RedisDataCacheClientSection GetConfiguration()
        {
            return  (RedisDataCacheClientSection)ConfigurationManager.GetSection(sectionName);
        }
    }
}
