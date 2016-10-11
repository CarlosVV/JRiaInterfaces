using System.Runtime.Serialization;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class GetProviderKeyRequest 
    {
        [DataMember(IsRequired = true)]
        public DataProvider DataProvider { get; set; }
    }
}