using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IBingPushPinParameterProvider
    {
        string GetPushPinParameter(ICollection<PushPinModel> pushPins);
    }
}