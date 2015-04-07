using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Factories;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.ApplicationServer.Caching;

namespace CES.CoreApi.Caching.Providers
{
    public class AppFabricCacheProvider : ICacheProvider
    {
        private readonly ILogMonitorFactory _monitorFactory;

        #region Core

        private const string AddItemMessageTemplate = "{0}: key='{1}', timeout='{2}', value='{3}'";
        private const string GetItemMessageTemplate = "{0}: key='{1}', timeout='{2}', getDataFunc='{3}'";
        private const string RemoveItemMessageTemplate = "{0}: key='{1}'";
        private const string ClearCacheMessageTemplate = "{0}";
        
        private static DataCache _cache;
        private static readonly DataCacheFactory CacheFactory;
        protected static readonly string CacheName;
        protected static readonly TimeSpan CacheLifetime;

        public AppFabricCacheProvider(ILogMonitorFactory monitorFactory, string cacheName = null)
        {
            if (monitorFactory == null) throw new ArgumentNullException("monitorFactory");
            _monitorFactory = monitorFactory;

            cacheName = string.IsNullOrEmpty(cacheName)
                ? CacheName
                : cacheName;

            _cache = !string.IsNullOrEmpty(cacheName)
                   ? CacheFactory.GetCache(cacheName)
                   : CacheFactory.GetDefaultCache();
        }

        static AppFabricCacheProvider()
        {
            CacheFactory = new DataCacheFactory(new DataCacheFactoryConfiguration());
            CacheName = ConfigurationManager.AppSettings["cacheName"];
            CacheLifetime = TimeSpan.Parse(ConfigurationManager.AppSettings["cacheLifetime"]);
        }

        #endregion

        #region Public methods

        public void AddItem(string key, object value)
        {
            AddItem(key, value, CacheLifetime);
        }

        public void AddItem(string key, object value, TimeSpan timeout)
        {
            var message = string.Format(CultureInfo.InvariantCulture, AddItemMessageTemplate,
                MethodBase.GetCurrentMethod().Name, key, timeout, value);

            try
            {
                if (timeout == default(TimeSpan))
                    timeout = CacheLifetime;

                var performanceMonitor = GetPerformanceMonitor(message);

                _cache.Add(key, value, timeout);

                performanceMonitor.Stop();
            }
            catch (DataCacheException ex)
            {
                PublishException(ex, message);
                throw;
            }
        }

        public T GetItem<T>(string key, Func<T> getDataFunc)
        {
            return GetItem(key, getDataFunc, CacheLifetime);
        }

        public T GetItem<T>(string key, Func<T> getDataFunc, TimeSpan timeout)
        {
            object result;
            var message = string.Format(CultureInfo.InvariantCulture, GetItemMessageTemplate,
                MethodBase.GetCurrentMethod().Name, key, timeout, getDataFunc);

            try
            {
                var performanceMonitor = GetPerformanceMonitor(message);

                result = _cache.Get(key);

                performanceMonitor.Stop();
            }
            catch (DataCacheException ex)
            {
                PublishException(ex, message);
                throw;
            }

            if (result != null)
                return (T) result;

            result = getDataFunc();

            if (result == null)
                return default(T);

            AddItem(key, result, timeout);

            return (T) result;
        }

        public void RemoveItem(string key)
        {
            var message = string.Format(CultureInfo.InvariantCulture, RemoveItemMessageTemplate,
                MethodBase.GetCurrentMethod().Name, key);

            try
            {
                var performanceMonitor = GetPerformanceMonitor(message);

                _cache.Remove(key);

                performanceMonitor.Stop();
            }
            catch (DataCacheException ex)
            {
                PublishException(ex, message);
                throw;
            }
        }

        public void ClearCache()
        {
            var message = string.Format(CultureInfo.InvariantCulture, ClearCacheMessageTemplate, MethodBase.GetCurrentMethod().Name);

            try
            {
                var performanceMonitor = GetPerformanceMonitor(message);

                Parallel.ForEach(_cache.GetSystemRegions(),
                    new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
                    region => _cache.ClearRegion(region));

                performanceMonitor.Stop();
            }
            catch (DataCacheException ex)
            {
                PublishException(ex, message);
                throw;
            }
          
        }
        
        #endregion

        #region private methods

        private void PublishException(Exception ex, string message)
        {
            var exceptionMonitor = _monitorFactory.CreateNew<IExceptionLogMonitor>();
            exceptionMonitor.Publish(ex, message);
        }
        
        private IPerformanceLogMonitor GetPerformanceMonitor(string message)
        {
            var performanceMonitor = _monitorFactory.CreateNew<IPerformanceLogMonitor>();
            performanceMonitor.Start(message);
            return performanceMonitor;
        }

        #endregion
    }
}
