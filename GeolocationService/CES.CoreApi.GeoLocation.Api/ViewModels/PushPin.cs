using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class PushPin 
    {
        [DataMember(IsRequired = true)]
        public Location Location { get; set; }

        [DataMember]
        public int IconStyle { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public PinColor PinColor { get; set; }
    }
}
