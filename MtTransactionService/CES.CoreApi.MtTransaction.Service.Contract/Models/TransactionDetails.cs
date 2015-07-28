using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class TransactionDetails : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime TransactionDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TransactionNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Customer Customer { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Beneficiary Beneficiary { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public MoneyTransferDetails MoneyTransferDetails { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ProcessingInformation ProcessingInformation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ComplianceInformation ComplianceInformation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public TransactionStatus TransactionStatus { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Agent ReceivingAgent { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Agent PayingAgent { get; set; }
    }
}