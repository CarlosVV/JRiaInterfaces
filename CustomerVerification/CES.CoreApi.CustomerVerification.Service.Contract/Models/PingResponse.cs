using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Service.Contract.Constants;

namespace CES.CoreApi.CustomerVerification.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerVerificationServiceDataContractNamespace)]
    public class PingResponse: BaseResponse
    {
        public PingResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public ICollection<DatabasePingModel> Databases { get; set; }
    }
}
