namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class MonetaryInformationModel
    {
        public decimal CommissionCustDiff { get; set; }
        public string CountryFrom { get; set; }
        public string CountryTo { get; set; }
        public decimal AmountLocal { get; set; }
        public decimal Rate { get; set; }
        public decimal BuyRate { get; set; }
        public decimal BuyRateFrom { get; set; }
        public decimal BuyRateTo { get; set; }
        public bool Inverted { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public string PaymentCurrency { get; set; }
        public decimal Commission { get; set; }
        public bool SpecialCommission { get; set; }
        public int ManualCommission { get; set; }
        public decimal Surcharge { get; set; }
        public int ProgramId { get; set; }
        public decimal OrderAmount { get; set; }
        public string BaseCurrency { get; set; }
        public decimal BaseRate { get; set; }
        public int RateLevel { get; set; }
        public decimal RateFrom { get; set; }
        public decimal RateTo { get; set; }
        public decimal PaymentRate { get; set; }
        public decimal PaymentBuyRate { get; set; }
        public bool PaymentRateInverted { get; set; }
        public decimal CommissionReceivingAgent { get; set; }
        public decimal CommissionReceivingAgentF { get; set; }
        public string CommissionReceivingAgentCurrency { get; set; }
    }
}