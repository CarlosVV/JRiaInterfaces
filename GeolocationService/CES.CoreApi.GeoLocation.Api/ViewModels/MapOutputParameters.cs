using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class MapOutputParameters 
    {
        [DataMember(IsRequired = true)]
        public int ZoomLevel { get; set; }

        [DataMember(IsRequired = true)]
        public MapImageFormat ImageFormat { get; set; }

        [DataMember(IsRequired = true)]
        public MapDisplayStyle MapStyle { get; set; }

    }
}
