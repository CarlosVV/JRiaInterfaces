namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class AmountDetailsModel
    {
        public decimal OrderAmount { get; set; }
        public decimal LocalAmount { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
