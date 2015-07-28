namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Models
{
    public class AmountDetailsModel
    {
        public decimal TransactionAmount { get; set; }
        public decimal LocalAmount { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
