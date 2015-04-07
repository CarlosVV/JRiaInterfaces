using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly IApplicationRepository _applicationRepository;
        private const string ClearCacheMessageSuccess = "Core API Geolocation service cache successfully cleaned up.";

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, IApplicationRepository applicationRepository)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (applicationRepository == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "applicationRepository");
            _cacheProvider = cacheProvider;
            _applicationRepository = applicationRepository;
        }

        #endregion

        #region Public methods

        public ClearCacheResponseModel ClearCache()
        {
            var response = new ClearCacheResponseModel();

            try
            {
                _cacheProvider.ClearCache();
                response.Message = ClearCacheMessageSuccess;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public HealthResponseModel Ping()
        {
            var response = new HealthResponseModel();

            try
            {
                _applicationRepository.Ping();
                response.MainDatabaseStatus = "OK";

            }
            catch (Exception ex)
            {
                response.MainDatabaseStatus = ex.Message;
            }

            return response;
        }

        #endregion
    }
}
