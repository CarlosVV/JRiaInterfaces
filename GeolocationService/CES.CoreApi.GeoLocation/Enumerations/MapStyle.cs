using CES.CoreApi.GeoLocation.Attributes;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations
{
    public enum MapStyle
    {
        Undefined = 0,
        [GoogleMapType("roadmap")]
        Road = 1,
        [GoogleMapType("satellite")]
        Aerial = 2,
        [GoogleMapType("hybrid")]
        AerialWithLabels = 3
    }
}