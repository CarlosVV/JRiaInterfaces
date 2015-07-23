namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class CurrencyDetailsModel
    {
        public string Currency { get; set; }
        public string PaymentCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public string CommissionReceivingAgentCurrency { get; set; }
    }
}
