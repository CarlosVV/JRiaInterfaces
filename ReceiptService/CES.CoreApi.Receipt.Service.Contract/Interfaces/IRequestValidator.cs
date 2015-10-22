using CES.CoreApi.Receipt.Service.Contract.Models;

namespace CES.CoreApi.Receipt.Service.Contract.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(ReceiptRequest request);
    }
}