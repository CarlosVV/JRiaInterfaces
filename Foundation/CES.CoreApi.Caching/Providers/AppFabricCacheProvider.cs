using System;
using System.Globalization;
using System.Threading.Tasks;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.ApplicationServer.Caching;

namespace CES.CoreApi.Caching.Providers
{
    public class AppFabricCacheProvider : CacheProviderBase
    {
        #region Core

        private readonly ILogManager _logManager;
        private static readonly DataCache Cache;

        public AppFabricCacheProvider(ILogManager logManager)
        {
            if (logManager == null) throw new ArgumentNullException("logManager");
            _logManager = logManager;
        }

        static AppFabricCacheProvider()
        {
            var cacheFactory = new DataCacheFactory(new DataCacheFactoryConfiguration());

            try
            {
                Cache = !string.IsNullOrEmpty(CacheName)
                    ? cacheFactory.GetCache(CacheName)
                    : cacheFactory.GetDefaultCache();
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
            // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        #endregion

        #region Public methods

        public override void AddCacheItem(string key, object value, TimeSpan timeout)
        {
            try
            {
                Cache.Add(key, value, timeout);
            }
            catch (DataCacheException ex)
            {
                _logManager.Publish(ex, string.Format(CultureInfo.InvariantCulture, "key={0}, value={1}, timeout={2}", key, value, timeout));
            }
        }

        public override T GetCacheItem<T>(string key, TimeSpan timeout, Func<T> getDataFunc)
        {
            object result = null;

            try
            {
                result = Cache.Get(key);
            }
            catch (Exception ex)
            {
                _logManager.Publish(ex, string.Format(CultureInfo.InvariantCulture,
                    "key={0}, timeout={1}, getDataFunc={2}", key, timeout, getDataFunc));
            }

            if (result != null) 
                return (T) result;

            result = getDataFunc();

            if (result == null) 
                return null;

            try
            {
                AddCacheItem(key, result, timeout);
            }
            catch (Exception ex)
            {
                _logManager.Publish(ex, string.Format(CultureInfo.InvariantCulture,
                    "AddCacheItem: key={0}, result={1}, timeout={2}", key, result, timeout));
            }

            return (T)result;
        }

        public override void RemoveCacheItem(string key)
        {
            Cache.Remove(key);
        }

        public override void ClearCache()
        {
            try
            {
                Parallel.ForEach(Cache.GetSystemRegions(),
                    new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                    region => Cache.ClearRegion(region));
            }
            catch (Exception ex)
            {
                _logManager.Publish(ex);
                throw;
            }
          
        }

        #endregion
    }
}
