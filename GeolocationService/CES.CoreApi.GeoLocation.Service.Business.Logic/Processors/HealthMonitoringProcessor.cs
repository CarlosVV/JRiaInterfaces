using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Caching.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Providers;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;


        public HealthMonitoringProcessor(ICacheProvider cacheProvider)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
       
            
            _cacheProvider = cacheProvider;
          
        }

        #endregion

        #region Public methods

        public ClearCacheResponseModel ClearCache()
        {
            var response = new ClearCacheResponseModel();

            try
            {
                _cacheProvider.ClearCache();
                response.IsOk = true;
            }
            catch (Exception)
            {
                response.IsOk = false;
            }

            return response;
        }

        public object Ping()
        {		

			return DatabasePingProvider.PingAllDatabases();
        }

        #endregion
    }
}
