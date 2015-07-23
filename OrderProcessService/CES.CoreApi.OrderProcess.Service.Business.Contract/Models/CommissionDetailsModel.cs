namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Models
{
    public class CommissionDetailsModel
    {
        public decimal Commission { get; set; }
        public bool SpecialCommission { get; set; }
        public int ManualCommission { get; set; }
        public decimal CommissionCustomerDiff { get; set; }
        public decimal CommissionReceivingAgentLocal { get; set; }
        public decimal CommissionReceivingAgentForeign { get; set; }
    }
}
