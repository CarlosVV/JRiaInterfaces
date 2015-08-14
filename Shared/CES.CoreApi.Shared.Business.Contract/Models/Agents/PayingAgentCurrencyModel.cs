namespace CES.CoreApi.Shared.Business.Contract.Models.Agents
{
    public class PayingAgentCurrencyModel
    {
        public string Currency { get; set; }

        public decimal Minimum { get; set; }

        public decimal Maximum { get; set; }

        public decimal DailyMaximum { get; set; }
    }
}
