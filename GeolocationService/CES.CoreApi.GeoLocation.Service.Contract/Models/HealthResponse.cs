using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class HealthResponse: BaseResponse
    {
        public HealthResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public string MainDatabaseStatus { get; set; }
    }
}
