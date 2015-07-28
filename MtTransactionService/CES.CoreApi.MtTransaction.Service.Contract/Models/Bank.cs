using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class Bank : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string BranchNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string BranchName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string SwiftCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string RoutingCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int RoutingType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Address Address { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int LocationId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ServiceLevelId { get; set; }
    }
}