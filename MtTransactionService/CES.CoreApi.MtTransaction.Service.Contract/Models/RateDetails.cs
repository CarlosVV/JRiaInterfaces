using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class RateDetails : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public decimal Rate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal BuyRate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal BuyRateFrom { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal BuyRateTo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool Inverted { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal BaseRate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int RateLevel { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal RateFrom { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal RateTo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal PaymentRate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal PaymentBuyRate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool PaymentRateInverted { get; set; }
    }
}