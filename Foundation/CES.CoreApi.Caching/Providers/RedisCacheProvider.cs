using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.ApplicationServer.Caching;
using StackExchange.Redis;
using CES.CoreApi.Caching.Configuration;
using CES.CoreApi.Caching.Helpers;
using Newtonsoft.Json;

namespace CES.CoreApi.Caching.Providers
{
    public class RedisCacheProvider : ICacheProvider
    {
        #region Core

        private readonly ILogMonitorFactory _monitorFactory;
        private readonly IIdentityManager _identityManager;
        
       

        private const string AddItemMessageTemplate = "{0}.{1}: key='{2}', timeout='{3}', value='{4}'";
        private const string GetItemMessageTemplate = "{0}.{1}: key='{2}', timeout='{3}', getDataFunc='{4}'";
        private const string RemoveItemMessageTemplate = "{0}.{1}: key='{2}'";
        private const string ClearCacheMessageTemplate = "{0}.{1}";
        private static readonly RedisDataCacheClientSection _config;
        private static  IDatabase  _database;


        public RedisCacheProvider(ILogMonitorFactory monitorFactory, IIdentityManager identityManager)
        {
            if (monitorFactory == null) throw new ArgumentNullException("monitorFactory");
            if (identityManager == null) throw new ArgumentNullException("identityManager");

            _monitorFactory = monitorFactory;
            _identityManager = identityManager;     
           
                    }

        static RedisCacheProvider()
        {
            _config = RedisCacheConnectionHelper.GetConfig();
            _database = RedisCacheConnectionHelper.GetDatabase();

        }


        #endregion

        #region Public methods

        public void AddItem(string key, object value)
        {
            AddItem(key, value, _config.CacheLifetime.Value);
        }

        public void AddItem(string key, object value, TimeSpan timeout)
        {
            var message = string.Format(CultureInfo.InvariantCulture, AddItemMessageTemplate,
                GetType().Name, MethodBase.GetCurrentMethod().Name, key, timeout, value);

            try
            {
                if (timeout == default(TimeSpan))
                    timeout = _config.CacheLifetime.Value;

                var performanceMonitor = GetPerformanceMonitor(message);
                _database.StringSet(key, JsonConvert.SerializeObject(value), timeout);
                
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
            return GetItem(key, getDataFunc, _config.CacheLifetime.Value);
        }

        public T GetItem<T>(string key, Func<T> getDataFunc, TimeSpan timeout, Func<T, bool> isCacheValid = null)
        {
            object result;
            string jsonResult = string.Empty;
            var message = string.Format(CultureInfo.InvariantCulture, GetItemMessageTemplate,
                GetType().Name, MethodBase.GetCurrentMethod().Name, key, timeout, getDataFunc);

            try
            {
                var performanceMonitor = GetPerformanceMonitor(message);
                jsonResult = _database.StringGet(key);
             
                performanceMonitor.Stop();
            }
            catch (DataCacheException ex)
            {
                PublishException(ex, message);
                throw;
            }

            if (jsonResult != null)
            {
                var typifiedResult = JsonConvert.DeserializeObject<T>(jsonResult);
                
                if (isCacheValid == null || isCacheValid(typifiedResult))
                    return JsonConvert.DeserializeObject<T>(jsonResult);
                RemoveItem(key);
            }

            result = getDataFunc();

            if (result == null)
                return default(T);

            AddItem(key, result, timeout);

            return (T) result;
        }

        public void RemoveItem(string key)
        {
            var message = string.Format(CultureInfo.InvariantCulture, RemoveItemMessageTemplate,
                GetType().Name, MethodBase.GetCurrentMethod().Name, key);

            try
            {
                var performanceMonitor = GetPerformanceMonitor(message);

                _database.KeyDelete(key);
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
            var message = string.Format(CultureInfo.InvariantCulture, ClearCacheMessageTemplate, GetType().Name, MethodBase.GetCurrentMethod().Name);

            try
            {
                var performanceMonitor = GetPerformanceMonitor(message);
              
                RedisCacheConnectionHelper.ClearCache();
       
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
            exceptionMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
            exceptionMonitor.Publish(ex, message);
        }

        private IPerformanceLogMonitor GetPerformanceMonitor(string message)
        {
            var performanceMonitor = _monitorFactory.CreateNew<IPerformanceLogMonitor>();
            performanceMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
            performanceMonitor.Start(message);
            return performanceMonitor;
        }

        #endregion
    }
}
