using CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Interfaces
{
    public interface ITransactionProcessor
    {
        TransactionDetailsModel GetDetails(int orderId, int databaseId, InformationGroup detalizationLevel);
        TransactionCreateModel CreateTransaction();
    }
}