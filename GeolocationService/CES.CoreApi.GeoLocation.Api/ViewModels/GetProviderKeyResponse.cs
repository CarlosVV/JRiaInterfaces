using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class GetProviderKeyResponse : BaseResponse
    {
       

        [DataMember]
        public string ProviderKey { get; set; }

        [DataMember]
        public bool IsValid { get; set; }
    }
}