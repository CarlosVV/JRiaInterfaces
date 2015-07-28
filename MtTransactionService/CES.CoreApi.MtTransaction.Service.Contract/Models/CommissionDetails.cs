using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class CommissionDetails : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public decimal Commission { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool SpecialCommission { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ManualCommission { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal CommissionCustomerDiff { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal CommissionReceivingAgentLocal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal CommissionReceivingAgentForeign { get; set; }
    }
}