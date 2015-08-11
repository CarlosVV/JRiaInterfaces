using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using CES.CoreApi.OrderValidation.Service.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Contract.Models;
using CES.CoreApi.OrderValidation.Service.Interfaces;
using Namespaces = CES.CoreApi.OrderValidation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.OrderValidation.Service
{
    [ServiceBehavior(Namespace = Namespaces.OrderValidationServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class OrderValidationService : IOrderValidationService
    {
        #region Core

        private readonly ITransactionValidateRequestProcessor _transactionValidateRequestProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public OrderValidationService(ITransactionValidateRequestProcessor transactionValidateRequestProcessor,
            IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (transactionValidateRequestProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "transactionValidateRequestProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");

            _transactionValidateRequestProcessor = transactionValidateRequestProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        #endregion

        #region IOrderValidationService implementation

        public async Task<OrderValidateResponse> ValidateOrder(OrderValidateRequest request)
        {
            _requestValidator.Validate(request);
            var requestModel = _mapper.ConvertTo<OrderValidateRequest, TransactionValidateRequestModel>(request);
            await _transactionValidateRequestProcessor.Validate(requestModel);

            //var responseModel = _addressServiceRequestProcessor.ValidateAddress(
            //    request.FormattedAddress,
            //    request.Country,
            //    _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            //return _mapper.ConvertToResponse<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
            return null;
        }

        #endregion
    }
}