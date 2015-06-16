using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.Settings.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly ICountryRepository _countryRepository;

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, ICountryRepository countryRepository)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (countryRepository == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "countryRepository");
        
            _cacheProvider = cacheProvider;
            _countryRepository = countryRepository;
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
            catch (Exception ex)
            {
                response.IsOk = false;
            }

            return response;
        }

        public HealthResponseModel Ping()
        {
            var response = new HealthResponseModel();

            response.Databases.Add(_countryRepository.Ping());

            return response;
        }

        #endregion
    }
}