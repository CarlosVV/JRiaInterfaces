using System;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Business.Logic.Processors
{
    public class HealthMonitoringProcessor : IHealthMonitoringProcessor
    {
        #region Core

        private readonly ICacheProvider _cacheProvider;
        private readonly IAgentCurrencyRepository _agentCurrencyRepository;

        public HealthMonitoringProcessor(ICacheProvider cacheProvider, IAgentCurrencyRepository agentCurrencyRepository)
        {
            if (cacheProvider == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
            if (agentCurrencyRepository == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "agentCurrencyRepository");

            _cacheProvider = cacheProvider;
            _agentCurrencyRepository = agentCurrencyRepository;
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

            response.Databases.Add(_agentCurrencyRepository.Ping());

            return response;
        }

        #endregion
    }
}