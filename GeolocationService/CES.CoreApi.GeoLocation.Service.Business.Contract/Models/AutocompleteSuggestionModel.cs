using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Models
{
    public class AutocompleteSuggestionModel
    {
        public AddressModel Address { get; set; }
        public LocationModel Location { get; set; }
        public LevelOfConfidence Confidence { get; set; }
    }
}