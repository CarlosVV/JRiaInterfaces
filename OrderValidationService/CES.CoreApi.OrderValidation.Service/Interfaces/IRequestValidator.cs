using CES.CoreApi.OrderValidation.Service.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(OrderValidateRequest request);
    }
}
