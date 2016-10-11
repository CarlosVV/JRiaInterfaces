using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Models
{
    public class AutocompleteSuggestionModel
    {
        public AddressModel Address { get; set; }
        public LocationModel Location { get; set; }
        public LevelOfConfidence Confidence { get; set; }
    }
}