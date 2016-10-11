using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class GetMapRequest 
    {
        [DataMember(IsRequired = true)]
        public Location Center { get; set; }

        [DataMember]
        public MapOutputParameters MapOutputParameters { get; set; }

        [DataMember(IsRequired = true)]
        public MapSize MapSize { get; set; }
        
        [DataMember]
        public Collection<PushPin> PushPins { get; set; }

        [DataMember(IsRequired = true)]
        public string Country { get; set; }
    }
}