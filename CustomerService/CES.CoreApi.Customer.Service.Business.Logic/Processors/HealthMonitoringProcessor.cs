using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Customer.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly ICustomerRepository _customerRepository;
        private readonly IApplicationRepository _applicationRepository;

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, ICustomerRepository customerRepository, 
            IApplicationRepository applicationRepository)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (customerRepository == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "customerRepository");
            if (applicationRepository == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "applicationRepository");

            _cacheProvider = cacheProvider;
            _customerRepository = customerRepository;
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

            response.Databases.Add(_applicationRepository.Ping());
            response.Databases.Add(_customerRepository.Ping());
                        
            return response;
        }

        #endregion
    }
}