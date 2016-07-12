using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class LogEventRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public DataProvider DataProvider { get; set; }

        [DataMember(IsRequired = true)]
        public string Message { get; set; }
    }
}