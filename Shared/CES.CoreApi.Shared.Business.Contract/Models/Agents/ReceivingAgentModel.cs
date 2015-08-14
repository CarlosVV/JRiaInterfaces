using CES.CoreApi.Shared.Business.Contract.Enumerations;

namespace CES.CoreApi.Shared.Business.Contract.Models.Agents
{
    public class ReceivingAgentModel: AgentModel
    {
        public ReceivingAgentStatus Status { get; set; }
    }
}