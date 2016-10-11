using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Models
{
    public class PushPinModel
    {
        public LocationModel Location { get; set; }
        public int? IconStyle { get; set; }
        public string Label { get; set; }
        public Color PinColor { get; set; }
    }
}
