using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IMapServiceRequestProcessor
    {
        GetMapResponseModel GetMap(string country, LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins);
    }
}