using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IGooglePushPinParameterProvider
    {
        string GetPushPinParameter(ICollection<PushPinModel> pushPins);
    }
}