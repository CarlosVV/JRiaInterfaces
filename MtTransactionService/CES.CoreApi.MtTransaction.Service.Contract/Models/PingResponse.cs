using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class PingResponse: BaseResponse
    {
        public PingResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public ICollection<DatabasePingResponse> Databases { get; set; }
    }
}
