using System.Runtime.Serialization;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract(Name = "Suggestion")]
    public class AutocompleteSuggestion 
    {
        [DataMember(EmitDefaultValue = false)]
        public AutocompleteAddress Address { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Location Location { get; set; }
        [DataMember]
        public Confidence Confidence { get; set; }
		[DataMember]
		public string ConfidenceText
		{
			get
			{
				return Confidence.ToString();
			}
		}
	}
}
