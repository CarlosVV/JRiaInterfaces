using System.Threading.Tasks;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces
{
    public interface ITransactionValidateRequestProcessor
    {
        Task Validate(TransactionValidateRequestModel customerId);
    }
}
