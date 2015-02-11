using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace, Name = "Suggestion")]
    public class AutocompleteSuggestion
    {
        [DataMember(EmitDefaultValue = false, IsRequired = true)]
        public AutocompleteAddress Address { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public Location Location { get; set; }
    }
}
