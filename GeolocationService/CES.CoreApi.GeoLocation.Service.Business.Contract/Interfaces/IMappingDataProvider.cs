using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IMappingDataProvider
    {
        GetMapResponseModel GetMap(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters,
            ICollection<PushPinModel> pushPins, DataProviderType providerType);
    }
}