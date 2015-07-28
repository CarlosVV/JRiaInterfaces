using CES.CoreApi.MtTransaction.Service.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(MtTransactionGetRequest request);
    }
}