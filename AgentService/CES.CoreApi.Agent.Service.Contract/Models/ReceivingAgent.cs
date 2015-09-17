using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class ReceivingAgent : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsOnHold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string OnHoldReason { get; set; }
    }
}