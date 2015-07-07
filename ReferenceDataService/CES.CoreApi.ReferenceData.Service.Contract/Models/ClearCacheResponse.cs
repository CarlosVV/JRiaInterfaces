using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;

namespace CES.CoreApi.ReferenceData.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ReferenceDataServiceDataContractNamespace)]
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
