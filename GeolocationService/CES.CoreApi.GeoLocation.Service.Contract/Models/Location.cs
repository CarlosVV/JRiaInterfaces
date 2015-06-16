using System.Runtime.Serialization;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class Location
    {
        /// <summary>
        /// Representing degrees of longitude.
        /// </summary>
        [DataMember(IsRequired = true)]
        public double Longitude { get; set; }

        /// <summary>
        /// Representing degrees of latitude.
        /// </summary>
        [DataMember(IsRequired = true)]
        public double Latitude { get; set; }
    }
}
