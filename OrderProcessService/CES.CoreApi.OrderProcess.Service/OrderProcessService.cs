using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;
using CES.CoreApi.OrderProcess.Service.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Contract.Models;
using CES.CoreApi.OrderProcess.Service.Interfaces;

namespace CES.CoreApi.OrderProcess.Service
{
    [ServiceBehavior(Namespace = Namespaces.OrderProcessServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class OrderProcessService : IOrderProcessService
    {
        #region Core

        private readonly ITransactionProcessor _transactionProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public OrderProcessService(ITransactionProcessor transactionProcessor, IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (transactionProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "TransactionProcessor");
            //if (healthMonitoringProcessor == null)
            //    throw new CoreApiException(TechnicalSubSystem.CustomerService,
            //        SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");

            _transactionProcessor = transactionProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        #endregion

        #region IOrderProcessService implementation

        public OrderGetResponse Get(OrderGetRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _transactionProcessor.GetDetails(request.OrderId, request.DatabaseId);
            return _mapper.ConvertToResponse<TransactionDetailsModel, OrderGetResponse>(responseModel);
        }

        public OrderCreateResponse Create(OrderCreateRequest request)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}