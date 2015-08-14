using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Agent.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Agent.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AgentServiceDataContractNamespace)]
    public class Contact : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public ICollection<Phone> PhoneList { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool? NoSms { get; set; }
    }
}