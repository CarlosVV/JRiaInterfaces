using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.LimitVerfication.Service.Contract.Constants;

namespace CES.CoreApi.LimitVerfication.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.LimitVerificationServiceDataContractNamespace)]
    public class CheckPayingAgentLimitsRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int AgentId { get; set; }
        [DataMember(IsRequired = true)]
        public int AgentLocationId { get; set; }
        [DataMember(IsRequired = true)]
        public string Currency { get; set; }
        [DataMember(IsRequired = true)]
        public decimal Amount { get; set; }
    }
}