using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class ComplianceInformation : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string TransferReason { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int TransferReasonId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ComplianceLines { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Question { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Answer { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool QuestionAskedOnBehalfOf { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string SourceOfFunds { get; set; }
    }
}