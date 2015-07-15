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

        private readonly IOrderProcessor _orderProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public OrderProcessService(IOrderProcessor orderProcessor, IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (orderProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "orderProcessor");
            //if (healthMonitoringProcessor == null)
            //    throw new CoreApiException(TechnicalSubSystem.CustomerService,
            //        SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");

            _orderProcessor = orderProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        #endregion

        #region IOrderProcessService implementation

        public OrderGetResponse Get(OrderGetRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _orderProcessor.GetOrder(request.OrderId, request.CheckMainDatabase, request.DatabaseId);
            return _mapper.ConvertToResponse<OrderModel, OrderGetResponse>(responseModel);
        }

        public OrderCreateResponse Create(OrderCreateRequest request)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}