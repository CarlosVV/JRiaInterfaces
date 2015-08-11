using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;

namespace CES.CoreApi.Agent.Service.Business.Contract.Models
{
    public class PayingAgentModel: AgentModel
    {
        public PayingAgentStatus Status { get; set; }
        public bool IsBeneficiaryLastName2Required { get; set; }
        public PayingAgentCurrencyModel AgentCurrencyInformation { get; set; }
        public PayingAgentCurrencyModel LocationCurrencyInformation { get; set; }
    }
}
