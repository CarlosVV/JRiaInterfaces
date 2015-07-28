using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class MtTransactionGetResponse: BaseResponse
    {
        public MtTransactionGetResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public TransactionDetails Transaction { get; set; }
    }
}