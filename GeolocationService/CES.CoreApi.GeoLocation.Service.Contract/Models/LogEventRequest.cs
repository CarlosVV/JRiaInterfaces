using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class LogEventRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public DataProvider DataProvider { get; set; }

        [DataMember(IsRequired = true)]
        public string Message { get; set; }
    }
}