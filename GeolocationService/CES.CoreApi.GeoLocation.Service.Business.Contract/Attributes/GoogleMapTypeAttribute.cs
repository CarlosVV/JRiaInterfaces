using System;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class GoogleMapTypeAttribute: Attribute
    {
        public string MapType { get; private set; }

        public GoogleMapTypeAttribute(string mapType)
        {
            MapType = mapType;
        }
    }
}
