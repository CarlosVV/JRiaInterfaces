using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Models
{
    public class MapOutputParametersModel
    {
        public int ZoomLevel { get; set; }

        public ImageFormat ImageFormat { get; set; }

        public MapStyle MapStyle { get; set; }

    }
}
