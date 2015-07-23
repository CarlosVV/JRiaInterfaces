using System;
using CES.CoreApi.Shared.Business.Contract.Enumerations;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class MoneyTransferDetailsModel
    {
        public int PreId { get; set; }
        public DeliveryMethodType DeliveryMethod { get; set; }
        public DateTime PaymentAvailableDate { get; set; }
        public bool IsOpenPayment { get; set; }
        public string Pin { get; set; }
        public int ReceivingAgentSequentialId { get; set; }
        public int PayingAgentSequentialId { get; set; }
        public BankDepositModel Deposit { get; set; }
        public MonetaryInformationModel MonetaryInformation { get; set; }
    }
}
