using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Caching.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly IDatabasePingProvider _pingProvider;

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, IDatabasePingProvider pingProvider)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (pingProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "pingProvider");
            _cacheProvider = cacheProvider;
            _pingProvider = pingProvider;
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
            return _pingProvider.PingDatabases();
        }

        #endregion
    }
}
