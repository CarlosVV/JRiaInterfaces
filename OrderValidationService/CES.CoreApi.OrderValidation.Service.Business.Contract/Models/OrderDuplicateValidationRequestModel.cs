namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class OrderDuplicateValidationRequestModel
    {
        public int RecAgentId { get; set; }

        public int RecAgentLocationId { get; set; }

        public int CustomerId { get; set; }

        public int PayAgentId { get; set; }

        public decimal AmountLocal { get; set; }

        public int Interval { get; set; }

        public string Currency { get; set; }
    }
}