using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class MapSize
    {
        [DataMember(IsRequired = true)]
        public int Width { get; set; }

        [DataMember(IsRequired = true)]
        public int Height { get; set; }
    }
}
