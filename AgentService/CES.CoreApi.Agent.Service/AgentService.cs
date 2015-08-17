using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Business.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Agent.Service.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Contract.Interfaces;
using CES.CoreApi.Agent.Service.Contract.Models;
using CES.CoreApi.Agent.Service.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;

namespace CES.CoreApi.Agent.Service
{
    [ServiceBehavior(Namespace = Namespaces.AgentServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AgentService : IAgentService, IAgentUserService, IHealthMonitoringService
    {
        #region Core

        private readonly IAgentProcessor _agentProcessor;
        private readonly IAgentUserProcessor _agentUserProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public AgentService(IAgentProcessor agentProcessor, IAgentUserProcessor agentUserProcessor, 
            IHealthMonitoringProcessor healthMonitoringProcessor, IMappingHelper mapper, 
            IRequestValidator requestValidator)
        {
            if (agentProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.AgentService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "agentProcessor");
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

            _agentProcessor = agentProcessor;
            _agentUserProcessor = agentUserProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        } 

        #endregion

        #region IAgentService implementation

        public async Task<GetPayingAgentResponse> GetPayingAgent(GetPayingAgentRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = await _agentProcessor.GetPayingAgent(
                request.AgentId,
                request.LocationId,
                request.Currency,
                _mapper.ConvertTo<PayingAgentInformationGroup, PayingAgentDetalizationLevel>(request.DetalizationLevel));
            var response = _mapper.ConvertToResponse<PayingAgentModel, GetPayingAgentResponse>(responseModel);
            return response;
        }

        public async Task<GetReceivingAgentResponse> GetReceivingAgent(GetReceivingAgentRequest request)
        {
            //_requestValidator.Validate(request);
            //var responseModel = await _agentProcessor.GetReceivingAgent(
            //    request.AgentId,
            //    request.LocationId,
            //    request.Currency,
            //    _mapper.ConvertTo<PayingAgentInformationGroup, PayingAgentDetalizationLevel>(request.DetalizationLevel));
            //var response = _mapper.ConvertToResponse<ReceivingAgentModel, GetReceivingAgentResponse>(responseModel);
            return null;
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