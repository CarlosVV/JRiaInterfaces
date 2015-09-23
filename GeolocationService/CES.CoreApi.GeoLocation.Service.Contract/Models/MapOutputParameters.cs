using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class MapOutputParameters : ExtensibleObject
    {
        [DataMember(IsRequired = true)]
        public int ZoomLevel { get; set; }

        [DataMember(IsRequired = true)]
        public MapImageFormat ImageFormat { get; set; }

        [DataMember(IsRequired = true)]
        public MapDisplayStyle MapStyle { get; set; }

    }
}
