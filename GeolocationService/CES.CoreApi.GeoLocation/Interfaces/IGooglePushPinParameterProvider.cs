using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IGooglePushPinParameterProvider
    {
        string GetPushPinParameter(ICollection<PushPinModel> pushPins);
    }
}