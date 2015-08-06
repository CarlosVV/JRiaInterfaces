using System.ServiceModel;
using System.Threading.Tasks;
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
    public class AgentService : IAgentCurrencyService, IAgentUserService, IHealthMonitoringService
    {
        #region Core

        private readonly IAgentCurrencyProcessor _currencyProcessor;
        private readonly IAgentUserProcessor _agentUserProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public AgentService(IAgentCurrencyProcessor currencyProcessor, IAgentUserProcessor agentUserProcessor, 
            IHealthMonitoringProcessor healthMonitoringProcessor, IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (currencyProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "currencyProcessor");
            if (agentUserProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "AgentUserProcessor");
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
            _agentUserProcessor = agentUserProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        } 

        #endregion

        #region IAgentCurrencyService implementation

        public async Task<GetAgentCurrencyResponse> GetAgentCurrency(GetAgentCurrencyRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = await _currencyProcessor.GetAgentCurrent(request.AgentId, request.Currency);
            return _mapper.ConvertToResponse<PayingAgentCurrencyModel, GetAgentCurrencyResponse>(responseModel);
        } 

        #endregion

        #region IHealthMonitoringService implementation

        public async Task<ClearCacheResponse> ClearCache()
        {
            var responseModel = await _healthMonitoringProcessor.ClearCache();
            return _mapper.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public async Task<PingResponse> Ping()
        {
            var responseModel = await _healthMonitoringProcessor.Ping();
            return _mapper.ConvertToResponse<PingResponseModel, PingResponse>(responseModel);
        }

        #endregion

        #region IAgentUserService implementation

        public async Task<ProcessSignatureResponse> ProcessSignature(ProcessSignatureRequest request)
        {
            _requestValidator.Validate(request);
            var requestModel = _mapper.ConvertTo<ProcessSignatureRequest, ProcessSignatureRequestModel>(request);
            var responseModel = await _agentUserProcessor.ProcessSignature(requestModel);
            return _mapper.ConvertToResponse<ProcessSignatureResponseModel, ProcessSignatureResponse>(responseModel);
        } 

        #endregion
    }
}