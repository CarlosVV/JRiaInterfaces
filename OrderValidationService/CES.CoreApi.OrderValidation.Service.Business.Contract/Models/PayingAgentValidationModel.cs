using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Models
{
    public class PayingAgentValidationModel
    {
        public bool IsOnHold { get; set; }
        public PayingAgentStatusType Status { get; set; }
        public int PayingAgentId { get; set; }
        public bool IsLocationOnHold { get; set; }
        public bool IsLocationDisabled { get; set; }
        public int LocationId { get; set; }
        public string BeneficiaryCurrency { get; set; }
        public decimal ReceiptAmount { get; set; }
        public bool IsDifferentCurrenciesSupported { get; set; }
        public decimal AgentCurrencyInfoDailyMaximum { get; set; }
        public decimal AgentCurrencyInfoMinimum { get; set; }
        public decimal AgentCurrencyInfoMaximum { get; set; }
        public decimal LocationCurrencyInfoDailyMaximum { get; set; }
        public decimal LocationCurrencyInfoMinimum { get; set; }
        public decimal LocationCurrencyInfoMaximum { get; set; }
    }
}