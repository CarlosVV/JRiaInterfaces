using CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Business.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Interfaces
{
    public interface ITransactionRepository
    {
        TransactionDetailsModel GetDetails(int orderId, InformationGroup detalizationLevel, int databaseId = 0);
    }
}