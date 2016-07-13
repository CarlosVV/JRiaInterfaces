using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class GetMapResponse : BaseResponse
    {
       

        [DataMember]
        public byte[]  MapData { get; set; }

        /// <summary>
        /// Returns true if map was received from data provider, otherwise false
        /// </summary>
        [DataMember]
        public bool IsValid { get; set; }

        /// <summary>
        /// Specify data provider used to get map
        /// </summary>
        [DataMember]
        public DataProvider DataProvider { get; set; }
    }
}