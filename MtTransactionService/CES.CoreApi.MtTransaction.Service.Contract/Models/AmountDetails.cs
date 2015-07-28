using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class AmountDetails : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public decimal TransactionAmount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal LocalAmount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal TransferAmount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal TaxAmount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal TotalAmount { get; set; }
    }
}