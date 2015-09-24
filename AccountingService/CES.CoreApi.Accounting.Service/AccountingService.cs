using System.ServiceModel;
using CES.CoreApi.Accounting.Service.Business.Contract.Interfaces;
using CES.CoreApi.Accounting.Service.Business.Contract.Models;
using CES.CoreApi.Accounting.Service.Contract.Constants;
using CES.CoreApi.Accounting.Service.Contract.Interfaces;
using CES.CoreApi.Accounting.Service.Contract.Models;
using CES.CoreApi.Accounting.Service.Interfaces;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Accounting.Service
{
    [ServiceBehavior(Namespace = Namespaces.AccountingServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AccountingService : ITransactionInformationService, IHealthMonitoringService
    {
        #region Core

        private readonly ITransactionInformationProcessor _transactionInformationProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public AccountingService(ITransactionInformationProcessor transactionInformationProcessor, IHealthMonitoringProcessor healthMonitoringProcessor, 
            IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (transactionInformationProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.AccountingService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "transactionInformationProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.AccountingService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.AccountingService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.AccountingService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");

            _transactionInformationProcessor = transactionInformationProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        } 

        #endregion

        #region ITransactionInformationService implementation

        public GetTransactionSummaryResponse GetTransactionSummary(GetTransactionSummaryRequest request)
        {
            _requestValidator.Validate(request);
            var requestModel = _mapper.ConvertTo<GetTransactionSummaryRequest, GetTransactionSummaryRequestModel>(request);
            var responseModel = _transactionInformationProcessor.GetTransactionSummary(requestModel);
            return _mapper.ConvertToResponse<TransactionSummaryModel, GetTransactionSummaryResponse>(responseModel);
        } 

        #endregion

        #region IHealthMonitoringService implementation

        public ClearCacheResponse ClearCache()
        {
            var responseModel = _healthMonitoringProcessor.ClearCache();
            return _mapper.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public HealthMonitoringResponse Ping()
        {
            var responseModel = _healthMonitoringProcessor.Ping();
            return null; //_mapper.ConvertToResponse<PingResponseModel, PingResponse>(responseModel);
        }

        #endregion
    }
}