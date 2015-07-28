using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class CurrencyDetails : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string Currency { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PaymentCurrency { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string BaseCurrency { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CommissionReceivingAgentCurrency { get; set; }
    }
}