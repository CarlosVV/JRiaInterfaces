namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class TransactionValidateRequestModel
    {
        public OrderAmountModel Amount { get; set; }

        public int PayingAgentId { get; set; }

        public int ReceivingAgentId { get; set; }

        public int CustomerId { get; set; }

        public BeneficiaryModel Beneficiary { get; set; }
    }
}
