using System;
using CES.CoreApi.Receipt.Service.Business.Contract.Interfaces;
using CES.CoreApi.Receipt.Service.Business.Contract.Models;
using System.Linq;

namespace CES.CoreApi.Receipt.Service.Business.Logic.Processors
{
    public class ReceiptProcessor : IReceiptProcessor
    {
        public ReceiptModel GenerateReceipt(int id)
        {
            byte[] body = new byte[5] { 0x00, 0x00, 0x00, 0x00, 0x00 };
            return new ReceiptModel { ReceiptName="Sender Receipts",ReceiptBody = body};
        }
    }
}
