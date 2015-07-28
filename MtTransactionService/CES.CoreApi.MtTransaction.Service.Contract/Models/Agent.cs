using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class Agent : ExtensibleObject
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
        public string AgentType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AgentLocation Location { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Number { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsRiaStore { get; set; }
    }
}