using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class AgentLocation: ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int Id { set; get; }
        [DataMember(EmitDefaultValue = false)]
        public string Name { set; get; }
        [DataMember(EmitDefaultValue = false)]
        public string BranchNumber { set; get; }
        [DataMember(EmitDefaultValue = false)]
        public Address Address { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int TimeZoneId { set; get; }
        [DataMember(EmitDefaultValue = false)]
        public double TimeZoneOffset { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool IsOnHold { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string OnHoldReason { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool IsDisabled { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Rating { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Contact Contact { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Note { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string NoteEnglish { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public AgentCurrency Currency { get; set; }

    }
}