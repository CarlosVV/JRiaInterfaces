using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.LimitVerification.Service.Business.Contract.Interfaces;
using CES.CoreApi.LimitVerification.Service.Business.Contract.Models;
using CES.CoreApi.LimitVerification.Service.Contract.Constants;
using CES.CoreApi.LimitVerification.Service.Contract.Interfaces;
using CES.CoreApi.LimitVerification.Service.Contract.Models;
using CES.CoreApi.LimitVerification.Service.Interfaces;

namespace CES.CoreApi.LimitVerification.Service
{
    [ServiceBehavior(Namespace = Namespaces.LimitVerificationServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LimitVerificationService : IAgentLimitVerificationService
    {
        #region Core

        private readonly IAgentLimitVerificationProcessor _agentLimitVerificationProcessor;
        private readonly IRequestValidator _requestValidator;
        private readonly IMappingHelper _mapper;

        public LimitVerificationService(IAgentLimitVerificationProcessor agentLimitVerificationProcessor, IRequestValidator requestValidator,
            IMappingHelper mapper)
        {
            if (agentLimitVerificationProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.LimitVerificationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "agentLimitVerificationProcessor");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.LimitVerificationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.LimitVerificationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");

            _agentLimitVerificationProcessor = agentLimitVerificationProcessor;
            _requestValidator = requestValidator;
            _mapper = mapper;
        } 

        #endregion

        #region IAgentLimitVerificationService implementation

        public CheckPayingAgentLimitsResponse CheckPayingAgentLimits(CheckPayingAgentLimitsRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _agentLimitVerificationProcessor.CheckPayingAgentLimits(request.AgentId, request.AgentLocationId, request.Currency, request.Amount);
            return _mapper.ConvertToResponse<CheckPayingAgentLimitsModel, CheckPayingAgentLimitsResponse>(responseModel);
        }

        public CheckReceivingAgentLimitsResponse CheckReceivingAgentLimits(CheckReceivingAgentLimitsRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _agentLimitVerificationProcessor.CheckReceivingAgentLimits(request.AgentId, request.UserId, request.Currency, request.Amount);
            return _mapper.ConvertToResponse<CheckReceivingAgentLimitsModel, CheckReceivingAgentLimitsResponse>(responseModel);
        } 

        #endregion
    }
}
