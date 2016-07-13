using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IMappingDataProvider
    {
        GetMapResponseModel GetMap(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters,
            ICollection<PushPinModel> pushPins, DataProviderType providerType);
    }
}