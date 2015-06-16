using System.Runtime.Serialization;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
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
