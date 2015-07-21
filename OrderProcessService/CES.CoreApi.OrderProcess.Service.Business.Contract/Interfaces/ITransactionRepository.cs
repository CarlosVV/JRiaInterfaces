using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces
{
    public interface ITransactionRepository
    {
        TransactionDetailsModel GetOrder(int orderId, int databaseId = 0);
    }
}