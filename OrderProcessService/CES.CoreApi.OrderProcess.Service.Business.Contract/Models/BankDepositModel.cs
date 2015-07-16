using System;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class BankDepositModel
    {
        public BankModel Bank { get; set; }
        public BankAccountModel Account { get; set; }
        public DateTime FulfillmentDate { get; set; }
        public int ProviderMapId { get; set; }
    }
}
