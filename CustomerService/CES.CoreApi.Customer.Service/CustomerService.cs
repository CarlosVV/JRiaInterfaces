using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Business.Contract.Interfaces;
using CES.CoreApi.Customer.Service.Business.Contract.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;
using CES.CoreApi.Customer.Service.Contract.Interfaces;
using CES.CoreApi.Customer.Service.Contract.Models;
using CES.CoreApi.Customer.Service.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Customer.Service
{
    [ServiceBehavior(Namespace = Namespaces.CustomerServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CustomerService : ICustomerService, IHealthMonitoringService
    {
        #region Core

        private readonly ICustomerRequestProcessor _customerRequestProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public CustomerService(ICustomerRequestProcessor customerRequestProcessor, IHealthMonitoringProcessor  healthMonitoringProcessor, 
            IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (customerRequestProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "customerRequestProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");

            _customerRequestProcessor = customerRequestProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        #endregion

        #region ICustomerService implementation

        public CustomerGetResponse Get(CustomerGetRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _customerRequestProcessor.GetCustomer(request.CustomerId);
            return _mapper.ConvertToResponse<CustomerModel, CustomerGetResponse>(responseModel);
        }

        public CustomerCreateResponse Create(CustomerCreateRequest request)
        {
            _requestValidator.Validate(request);
            //var responseModel = _customerRequestProcessor.GetCustomer(request.CustomerId);
            //return _mapper.ConvertToResponse<CustomerModel, CustomerCreateResponse>(responseModel);
            return null;
        }

        #endregion

        #region IHealthMonitoringService implementation

        public ClearCacheResponse ClearCache()
        {
            var responseModel = _healthMonitoringProcessor.ClearCache();
            return _mapper.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public HealthResponse Ping()
        {
            var responseModel = _healthMonitoringProcessor.Ping();
            return _mapper.ConvertToResponse<HealthResponseModel, HealthResponse>(responseModel);
        }

        #endregion
    }
}