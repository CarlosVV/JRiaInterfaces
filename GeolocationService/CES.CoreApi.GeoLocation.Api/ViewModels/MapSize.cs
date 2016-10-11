using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class MapSize 
    {
        [DataMember(IsRequired = true)]
        public int Width { get; set; }

        [DataMember(IsRequired = true)]
        public int Height { get; set; }
    }
}
