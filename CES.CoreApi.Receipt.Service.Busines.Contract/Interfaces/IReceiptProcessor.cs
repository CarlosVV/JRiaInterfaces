using CES.CoreApi.Receipt.Service.Business.Contract.Models;

namespace CES.CoreApi.Receipt.Service.Business.Contract.Interfaces
{
    public interface IReceiptProcessor
    {
        ReceiptModel GenerateReceipt(int id);
    }
}