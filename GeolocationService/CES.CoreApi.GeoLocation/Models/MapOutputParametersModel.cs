using CES.CoreApi.GeoLocation.Enumerations;


namespace CES.CoreApi.GeoLocation.Models
{
    public class MapOutputParametersModel
    {
        public int ZoomLevel { get; set; }

        public ImageFormat ImageFormat { get; set; }

        public MapStyle MapStyle { get; set; }

    }
}
