using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace, Name = "Suggestion")]
    public class AutocompleteSuggestion : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false, IsRequired = true)]
        public AutocompleteAddress Address { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Location Location { get; set; }
        [DataMember]
        public Confidence Confidence { get; set; }
    }
}
