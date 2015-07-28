using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class MoneyTransferDetails : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public int PreId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DeliveryMethod { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime PaymentAvailableDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsOpenPayment { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Pin { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ReceivingAgentSequentialId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int PayingAgentSequentialId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public BankDeposit Deposit { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public MonetaryInformation MonetaryInformation { get; set; }
    }
}