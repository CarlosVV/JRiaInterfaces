using System.Runtime.Serialization;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace, Name = "Suggestion")]
    public class AutocompleteSuggestion 
    {
        [DataMember(EmitDefaultValue = false)]
        public AutocompleteAddress Address { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Location Location { get; set; }
        [DataMember]
        public Confidence Confidence { get; set; }
		[DataMember]
		public string ConfidenceText {
			get
			{
				return Confidence.ToString();
			}
		}
	}
}
