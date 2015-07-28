using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Interfaces;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;
using CES.CoreApi.MtTransaction.Service.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Contract.Interfaces;
using CES.CoreApi.MtTransaction.Service.Contract.Models;
using CES.CoreApi.MtTransaction.Service.Interfaces;

namespace CES.CoreApi.MtTransaction.Service
{
    [ServiceBehavior(Namespace = Namespaces.MtTransactionServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MtTransactionService : IMtTransactionService, IHealthMonitoringService
    {
        #region Core

        private readonly ITransactionProcessor _transactionProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _requestValidator;

        public MtTransactionService(ITransactionProcessor transactionProcessor,  IHealthMonitoringProcessor  healthMonitoringProcessor, 
            IMappingHelper mapper, IRequestValidator requestValidator)
        {
            if (transactionProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.MtTransactionService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "TransactionProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.MtTransactionService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.MtTransactionService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.MtTransactionService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");

            _transactionProcessor = transactionProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _mapper = mapper;
            _requestValidator = requestValidator;
        }

        #endregion

        #region IMtTransactionService implementation

        public MtTransactionGetResponse Get(MtTransactionGetRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _transactionProcessor.GetDetails(request.TransactionId, request.DatabaseId, _mapper.ConvertTo<TransactionInformationGroup, InformationGroup>(request.DetalizationLevel));
            var response = _mapper.ConvertToResponse<TransactionDetailsModel, MtTransactionGetResponse>(responseModel);
            return response;
        }

        public MtTransactionCreateResponse Create(MtTransactionCreateRequest request)
        {
            throw new System.NotImplementedException();
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