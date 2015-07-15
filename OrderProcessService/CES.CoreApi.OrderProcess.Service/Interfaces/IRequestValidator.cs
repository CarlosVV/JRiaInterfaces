using CES.CoreApi.OrderProcess.Service.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(OrderGetRequest request);
    }
}