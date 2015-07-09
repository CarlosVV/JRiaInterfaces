using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces
{
    public interface IValidationMainRepository
    {
        bool IsDuplicateOrderExist(OrderDuplicateValidationRequestModel model);
    }
}