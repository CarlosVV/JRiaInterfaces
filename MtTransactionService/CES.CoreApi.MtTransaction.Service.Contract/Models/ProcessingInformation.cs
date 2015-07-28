using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class ProcessingInformation : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int DatabaseId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int TerminalId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsIndefinite { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DelayMinutes { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int EnteredByLoginId { get; set; }
    }
}