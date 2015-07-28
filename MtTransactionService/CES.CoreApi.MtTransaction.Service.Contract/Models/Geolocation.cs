using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class Geolocation : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public double? Longitude { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double? Latitude { get; set; }
    }
}