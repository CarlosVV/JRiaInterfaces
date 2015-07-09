using CES.CoreApi.Customer.Service.Business.Contract.Models;

namespace CES.CoreApi.Customer.Service.Business.Contract.Interfaces
{
    public interface ICustomerRequestProcessor
    {
        CustomerModel GetCustomer(int customerId);
        ProcessSignatureModel ProcessSignature(int orderId, byte[] signature);
    }
}