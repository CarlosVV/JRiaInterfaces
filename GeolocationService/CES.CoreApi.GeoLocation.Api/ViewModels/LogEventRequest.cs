using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class LogEventRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public DataProvider DataProvider { get; set; }

        [DataMember(IsRequired = true)]
        public string Message { get; set; }
    }
}