using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Constants;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class PingResponse: BaseResponse
    {
       

        [DataMember]
        public ICollection<DatabasePingResponse> Databases { get; set; }
    }
}
