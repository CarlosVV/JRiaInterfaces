using CES.CoreApi.Accounting.Service.Business.Contract.Interfaces;
using CES.CoreApi.Accounting.Service.Business.Contract.Models;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;

namespace CES.CoreApi.Accounting.Service.Business.Logic.Processors
{
    public class TransactionInformationProcessor : ITransactionInformationProcessor
    {
        #region Core

        private readonly ITransactionInformationRepository _repository;

        public TransactionInformationProcessor(ITransactionInformationRepository repository)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.AccountingService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            _repository = repository;
        } 

        #endregion


        public TransactionSummaryModel GetTransactionSummary(GetTransactionSummaryRequestModel request)
        {
            return _repository.GetSummaryByAgentLocation(request);
        }
    }
}
