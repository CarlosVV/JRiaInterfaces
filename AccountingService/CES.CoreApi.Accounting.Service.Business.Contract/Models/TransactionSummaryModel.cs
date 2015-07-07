namespace CES.CoreApi.Accounting.Service.Business.Contract.Models
{
    public class TransactionSummaryModel
    {
        public decimal TransferTotal { get; set; }
        public decimal UsdTotal { get; set; }
        public decimal LocalUsdTotal { get; set; }
        public decimal AmountTotal { get; set; }
    }
}