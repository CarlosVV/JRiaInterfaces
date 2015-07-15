using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces
{
    public interface IOrderProcessor
    {
        OrderModel GetOrder(int orderId, bool checkMainDatabase, int databaseId);
    }
}