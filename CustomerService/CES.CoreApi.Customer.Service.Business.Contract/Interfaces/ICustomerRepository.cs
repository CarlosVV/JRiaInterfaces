using System.Threading.Tasks;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Customer.Service.Business.Contract.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerModel> GetCustomer(int customerId);
    }
}