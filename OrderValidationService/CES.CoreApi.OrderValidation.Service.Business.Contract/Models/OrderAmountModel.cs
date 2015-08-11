namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class OrderAmountModel
    {
        public decimal LocalAmount { get; set; }

        public decimal ForeignAmount { get; set; }

        public string CurrencyFrom { get; set; }

        public string CurrencyTo { get; set; }

        public decimal CustomerRate { get; set; }

        public decimal CustomerFee { get; set; }

        public decimal SurchargeFee { get; set; }

        public decimal Tax { get; set; }

        public bool ManualCustomerFee { get; set; }
    }
}