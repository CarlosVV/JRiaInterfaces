using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class PushPin
    {
        [DataMember(IsRequired = true)]
        public Location Location { get; set; }

        [DataMember]
        public int IconStyle { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public PinColor PinColor { get; set; }
    }
}
