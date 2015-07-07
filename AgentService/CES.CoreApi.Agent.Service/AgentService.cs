using System.ServiceModel;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Agent.Service.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Contract.Models;
using CES.CoreApi.Agent.Service.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Agent.Service
{
    [ServiceBehavior(Namespace = Namespaces.AgentServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AgentService : IAgentCurrencyService, IHealthMonitoringService
    {
        #region Core

        private readonly IAgentCurrencyProcessor _currencyProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public AgentService(IAgentCurrencyProcessor currencyProcessor, IHealthMonitoringProcessor healthMonitoringProcessor, 
            IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (currencyProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "currencyProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");

            _currencyProcessor = currencyProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        } 

        #endregion

        #region IAgentCurrencyService implementation

        public GetAgentCurrencyResponse GetAgentCurrency(GetAgentCurrencyRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _currencyProcessor.GetAgentCurrent(request.AgentId, request.Currency);
            return _mapper.ConvertToResponse<PayingAgentCurrencyModel, GetAgentCurrencyResponse>(responseModel);
        } 

        #endregion

        #region IHealthMonitoringService implementation

        public ClearCacheResponse ClearCache()
        {
            var responseModel = _healthMonitoringProcessor.ClearCache();
            return _mapper.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public PingResponse Ping()
        {
            var responseModel = _healthMonitoringProcessor.Ping();
            return _mapper.ConvertToResponse<PingResponseModel, PingResponse>(responseModel);
        }

        #endregion
    }
}