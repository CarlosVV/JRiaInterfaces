using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class GetProviderKeyRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public DataProvider DataProvider { get; set; }
    }
}