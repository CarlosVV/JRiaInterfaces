using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.ReferenceData.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor 
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly IReferenceDataRepository _referenceDataRepository;

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, IReferenceDataRepository referenceDataRepository)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.ReferenceDataService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (referenceDataRepository == null)
                throw new CoreApiException(TechnicalSubSystem.ReferenceDataService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "referenceDataRepository");

            _cacheProvider = cacheProvider;
            _referenceDataRepository = referenceDataRepository;
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
            var response = new PingResponseModel();

            response.Databases.Add(_referenceDataRepository.Ping());

            return response;
        }

        #endregion
    }
}