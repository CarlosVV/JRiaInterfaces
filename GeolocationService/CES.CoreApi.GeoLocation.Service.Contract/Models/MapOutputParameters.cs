using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class MapOutputParameters
    {
        [DataMember(IsRequired = true)]
        public int ZoomLevel { get; set; }

        [DataMember(IsRequired = true)]
        public MapImageFormat ImageFormat { get; set; }

        [DataMember(IsRequired = true)]
        public MapDisplayStyle MapStyle { get; set; }

    }
}
