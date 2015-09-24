using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Interfaces;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Business.Logic.Processors
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly ITransactionRepository _transactionRepository;

        #region Core

        public TransactionProcessor(ITransactionRepository transactionRepository)
        {
            if (transactionRepository == null)
                throw new CoreApiException(TechnicalSubSystem.MtTransactionService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "TransactionRepository");
            _transactionRepository = transactionRepository;
        }

        #endregion

        #region ITransactionProcessor implementation

        public TransactionDetailsModel GetDetails(int orderId, int databaseId, InformationGroup detalizationLevel)
        {
            return _transactionRepository.GetDetails(orderId, detalizationLevel, databaseId);
        }

        public TransactionCreateModel CreateTransaction(int customerId)
        {
            return null;
        }

        #endregion
    }
}
