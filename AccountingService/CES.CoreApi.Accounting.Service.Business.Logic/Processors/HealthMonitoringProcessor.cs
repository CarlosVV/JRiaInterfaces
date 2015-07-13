using System;
using CES.CoreApi.Accounting.Service.Business.Contract.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly ITransactionInformationRepository _transactionInformationRepository;

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, ITransactionInformationRepository transactionInformationRepository)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.AccountingService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (transactionInformationRepository == null)
                throw new CoreApiException(TechnicalSubSystem.AccountingService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "transactionInformationRepository");

            _cacheProvider = cacheProvider;
            _transactionInformationRepository = transactionInformationRepository;
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

        public HealthResponseModel Ping()
        {
            var response = new HealthResponseModel();

            response.Databases.Add(_transactionInformationRepository.Ping());

            return response;
        }

        #endregion
    }
}