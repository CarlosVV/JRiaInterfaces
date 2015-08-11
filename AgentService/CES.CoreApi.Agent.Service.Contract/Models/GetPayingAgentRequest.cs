using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Agent.Service.Contract.Enumerations;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class GetPayingAgentRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int AgentId { get; set; }

        [DataMember(IsRequired = false)]
        public int LocationId { get; set; }

        [DataMember(IsRequired = false)]
        public string CurrencySymbol { get; set; }

        [DataMember(IsRequired = true)]
        public AgentInformationGroup DetalizationLevel { get; set; }
    }
}