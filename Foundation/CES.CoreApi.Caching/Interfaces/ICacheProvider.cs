using System;

namespace CES.CoreApi.Caching.Interfaces
{
    public interface ICacheProvider
    {
        void AddCacheItem(string key, object value);
        void AddCacheItem(string key, object value, TimeSpan timeout);
        T GetCacheItem<T>(string key, Func<T> getDataFunc) where T : class;
        T GetCacheItem<T>(string key, TimeSpan timeout, Func<T> getDataFunc) where T : class;
        void RemoveCacheItem(string key);
        void ClearCache();
    }
}
