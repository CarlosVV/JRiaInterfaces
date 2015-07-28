using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class TaxInformation: ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string TaxId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Ssn { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TaxCountry { get; set; }
    }
}