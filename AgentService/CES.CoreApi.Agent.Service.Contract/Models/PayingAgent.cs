using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class PayingAgent : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ShortName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DepartmentId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<AgentLocation> Locations { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsRiaStore { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsOnHold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string OnHoldReason { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsBeneficiaryLastName2Required { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AgentCurrency Currency { get; set; }
    }
}