using System.Runtime.Serialization;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class MapSize
    {
        [DataMember(IsRequired = true)]
        public int Width { get; set; }

        [DataMember(IsRequired = true)]
        public int Height { get; set; }
    }
}
