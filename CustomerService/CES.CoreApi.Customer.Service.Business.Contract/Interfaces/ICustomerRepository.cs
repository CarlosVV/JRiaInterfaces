using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Customer.Service.Business.Contract.Interfaces
{
    public interface ICustomerRepository
    {
        CustomerModel GetCustomer(int customerId);
    }
}