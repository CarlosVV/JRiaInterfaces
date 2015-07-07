namespace CES.CoreApi.Agent.Service.Business.Contract.Models
{
    public class PayingAgentCurrencyModel
    {
        public int Id { get; set; }

        public string Currency { get; set; }

        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; }

        public decimal DailyMaximum { get; set; }
    }
}
