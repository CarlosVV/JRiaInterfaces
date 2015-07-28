using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class TransactionStatus : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public bool NeedsApproval { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int EnteredBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ConfirmedBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime ConfirmedTime { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CancelledBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime CancelledTime { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool Posted { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int OnHold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool ReadyForPosting { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool LegalHold { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EntryType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime EnteredTime { get; set; }
    }
}
