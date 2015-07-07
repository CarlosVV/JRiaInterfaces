namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class OrderDuplicateValidationRequestModel
    {
        public int ReceivingAgentId { get; set; }

        public int ReceivingAgentLocationId { get; set; }

        public int CustomerId { get; set; }

        public int PayAgentId { get; set; }

        public decimal AmountLocal { get; set; }

        public int Interval { get; set; }

        public string Currency { get; set; }
    }
}