namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Models
{
    public class RateDetailsModel
    {
        public decimal Rate { get; set; }
        public decimal BuyRate { get; set; }
        public decimal BuyRateFrom { get; set; }
        public decimal BuyRateTo { get; set; }
        public bool Inverted { get; set; }
        public decimal BaseRate { get; set; }
        public int RateLevel { get; set; }
        public decimal RateFrom { get; set; }
        public decimal RateTo { get; set; }
        public decimal PaymentRate { get; set; }
        public decimal PaymentBuyRate { get; set; }
        public bool PaymentRateInverted { get; set; }
    }
}
