using System;
using System.Configuration;
using CES.CoreApi.Caching.Interfaces;

namespace CES.CoreApi.Caching.Providers
{
    public abstract class CacheProviderBase : ICacheProvider
    {
        protected static readonly string CacheName = ConfigurationManager.AppSettings["cacheName"];
        protected static readonly TimeSpan CacheLifetime = TimeSpan.Parse(ConfigurationManager.AppSettings["cacheLifetime"]);

        public void AddCacheItem(string key, object value)
        {
            AddCacheItem(key, value, CacheLifetime);
        }

        public abstract void AddCacheItem(string key, object value, TimeSpan timeout);

        public T GetCacheItem<T>(string key, Func<T> getDataFunc) where T : class
        {
            return GetCacheItem(key, CacheLifetime, getDataFunc);
        }

        public abstract T GetCacheItem<T>(string key, TimeSpan timeout, Func<T> getDataFunc) where T : class;

        public abstract void RemoveCacheItem(string key);

        public abstract void ClearCache();
    }
}
