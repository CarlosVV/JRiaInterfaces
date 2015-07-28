using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class AgentLocation : ExtensibleObject
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
    }
}