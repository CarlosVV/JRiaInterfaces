using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Business.Contract.Interfaces;
using CES.CoreApi.CustomerVerification.Business.Contract.Models;
using CES.CoreApi.CustomerVerification.Service.Contract.Constants;
using CES.CoreApi.CustomerVerification.Service.Contract.Interfaces;
using CES.CoreApi.CustomerVerification.Service.Contract.Models;
using CES.CoreApi.CustomerVerification.Service.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.CustomerVerification.Service
{
    [ServiceBehavior(Namespace = Namespaces.CustomerVerificationServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CustomerVerificationService: ICustomerVerificationService, IHealthMonitoringService
    {
        #region Core

        private readonly ICustomerVerificationRequestProcessor _customerVerificationRequestProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public CustomerVerificationService(ICustomerVerificationRequestProcessor customerVerificationRequestProcessor, IHealthMonitoringProcessor healthMonitoringProcessor,
            IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (customerVerificationRequestProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "customerVerificationRequestProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");
            
            _customerVerificationRequestProcessor = customerVerificationRequestProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        #endregion

        public async Task<VerifyCustomerIdentityResponse> VerifyCustomerIdentity(VerifyCustomerIdentityRequest request)
        {
            _requestValidator.Validate(request);
            var requestModel = _mapper.ConvertTo<VerifyCustomerIdentityRequest, VerifyCustomerIdentityRequestModel>(request);
            var responseModel = await _customerVerificationRequestProcessor.VerifyCustomerIdentity(requestModel);
            return null;// _mapper.ConvertToResponse<CustomerModel, CustomerGetResponse>(responseModel);
        }

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
    }
}
