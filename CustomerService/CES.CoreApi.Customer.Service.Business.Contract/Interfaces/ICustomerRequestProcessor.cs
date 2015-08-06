using System.Threading.Tasks;
using CES.CoreApi.Customer.Service.Business.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Customer.Service.Business.Contract.Interfaces
{
    public interface ICustomerRequestProcessor
    {
        Task<CustomerModel> GetCustomer(int customerId);
        Task<ProcessSignatureModel> ProcessSignature(int orderId, byte[] signature);
    }
}