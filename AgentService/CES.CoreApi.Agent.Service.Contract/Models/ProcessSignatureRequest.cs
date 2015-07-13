using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class ProcessSignatureRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int AgentId { get; set; }
        [DataMember(IsRequired = true)]
        public int LocationId { get; set; }
        [DataMember(IsRequired = true)]
        public int UserId { get; set; }
        [DataMember(IsRequired = true)]
        public byte[] Signature { get; set; }
    }
}