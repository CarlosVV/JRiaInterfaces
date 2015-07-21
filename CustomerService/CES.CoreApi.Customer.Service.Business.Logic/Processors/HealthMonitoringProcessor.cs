using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Interfaces;

namespace CES.CoreApi.Customer.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly IDatabasePingProvider _pingProvider;

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, IDatabasePingProvider pingProvider)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (pingProvider == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
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

        public PingResponseModel Ping()
        {
            return _pingProvider.PingDatabases();
        }

        #endregion
    }
}