using CES.CoreApi.Receipt.Service.Contract.Models;

namespace CES.CoreApi.Receipt.Service.Interfaces
{
    public interface IRequestValidator
    {
        void Validate(ReceiptRequest request);
    }
}