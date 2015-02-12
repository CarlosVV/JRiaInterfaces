using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class GetProviderKeyResponse : BaseResponse
    {
        public GetProviderKeyResponse(ICurrentDateTimeProvider currentDateTimeProvider)
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public string ProviderKey { get; set; }

        [DataMember]
        public bool IsValid { get; set; }
    }
}