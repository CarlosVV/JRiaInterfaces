using System.Collections.Generic;
using CES.CoreApi.Shared.Business.Contract.Enumerations;

namespace CES.CoreApi.Shared.Business.Contract.Models.Agents
{
    public class PayingAgentModel: AgentModel
    {
        public PayingAgentStatus Status { get; set; }
        public bool IsBeneficiaryLastName2Required { get; set; }
        public PayingAgentCurrencyModel Currency { get; set; }
        public IEnumerable<PayingAgentLocationModel> Locations { get; set; }
    }
}
