using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class BankDeposit : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public Bank Bank { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public BankAccount Account { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FulfillmentDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ProviderMapId { get; set; }
    }
}