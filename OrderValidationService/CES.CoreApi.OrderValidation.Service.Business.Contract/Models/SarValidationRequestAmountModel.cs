namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class SarValidationRequestAmountModel
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
        public decimal Total { get; set; }
    }
}