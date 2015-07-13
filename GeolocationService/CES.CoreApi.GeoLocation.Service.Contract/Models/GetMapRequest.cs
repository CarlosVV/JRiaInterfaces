using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class GetMapRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public Location Center { get; set; }

        [DataMember]
        public MapOutputParameters MapOutputParameters { get; set; }

        [DataMember(IsRequired = true)]
        public MapSize MapSize { get; set; }
        
        [DataMember]
        public Collection<PushPin> PushPins { get; set; }

        [DataMember(IsRequired = true)]
        public string Country { get; set; }
    }
}