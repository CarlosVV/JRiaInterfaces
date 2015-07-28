using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class MonetaryInformation : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string CountryFrom { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CountryTo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CommissionDetails CommissionDetails { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AmountDetails AmountDetails { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public RateDetails RateDetails { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public CurrencyDetails CurrencyDetails { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public decimal Surcharge { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ProgramId { get; set; }
    }
}