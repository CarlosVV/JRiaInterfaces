using System;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Data.Interfaces;

namespace CES.CoreApi.CustomerVerification.Business.Logic.Processors
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

        public async Task<ClearCacheResponseModel> ClearCache()
        {
            return await Task.Run(() =>
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
            });
        }

        public async Task<PingResponseModel> Ping()
        {
            return await Task.Run(() => _pingProvider.PingDatabases());
        }

        #endregion
    }
}