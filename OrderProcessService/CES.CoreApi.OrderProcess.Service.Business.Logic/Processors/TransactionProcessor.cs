using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Business.Logic.Processors
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly ITransactionRepository _transactionRepository;

        #region Core

        public TransactionProcessor(ITransactionRepository transactionRepository)
        {
            if (transactionRepository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderProcessService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "TransactionRepository");
            _transactionRepository = transactionRepository;
        }

        #endregion

        #region ITransactionProcessor implementation
        
        public TransactionDetailsModel GetOrder(int orderId, bool checkMainDatabase, int databaseId)
        {
            return _transactionRepository.GetOrder(orderId);
        } 

        #endregion
    }
}
