using CES.CoreApi.Customer.Service.Contract.Models;

namespace CES.CoreApi.Customer.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(CustomerGetRequest request);
        void Validate(CustomerCreateRequest request);
    }
}