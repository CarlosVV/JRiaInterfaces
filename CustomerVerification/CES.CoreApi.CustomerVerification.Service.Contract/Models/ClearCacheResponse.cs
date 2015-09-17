using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Service.Contract.Constants;

namespace CES.CoreApi.CustomerVerification.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerVerificationServiceDataContractNamespace)]
    public class ClearCacheResponse : BaseResponse
    {
        public ClearCacheResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public string Message { get; set; }
    }
}
