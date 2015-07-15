using CES.CoreApi.OrderProcess.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Interfaces
{
    public interface IOrderRepository
    {
        OrderModel GetOrder(int orderId);
    }
}