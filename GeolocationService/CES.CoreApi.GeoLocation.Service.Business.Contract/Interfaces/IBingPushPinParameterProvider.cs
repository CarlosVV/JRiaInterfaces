using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IBingPushPinParameterProvider
    {
        string GetPushPinParameter(ICollection<PushPinModel> pushPins);
    }
}