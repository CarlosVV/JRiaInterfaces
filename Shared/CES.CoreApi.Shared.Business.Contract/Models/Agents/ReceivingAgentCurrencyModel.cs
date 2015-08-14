namespace CES.CoreApi.Shared.Business.Contract.Models.Agents
{
    public class ReceivingAgentCurrencyModel
    {
        public int BranchId { get; set; }
        public string Currency { get; set; }
        public bool IsDefault { get; set; }
        public bool IsExcluded { get; set; }
    }
}
