using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Models
{
    public class PushPinModel
    {
        public LocationModel Location { get; set; }
        public int? IconStyle { get; set; }
        public string Label { get; set; }
        public Color PinColor { get; set; }
    }
}
