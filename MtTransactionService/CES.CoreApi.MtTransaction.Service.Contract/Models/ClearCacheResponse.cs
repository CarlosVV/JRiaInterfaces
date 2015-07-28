using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
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
