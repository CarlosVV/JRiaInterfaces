using System.Runtime.Serialization;
using CES.CoreApi.Accounting.Service.Contract.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AccountingServiceDataContractNamespace)]
    public class GetTransactionSummaryResponse: BaseResponse
    {
        public GetTransactionSummaryResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
            
        }

        [DataMember(EmitDefaultValue = false)]
        public TransactionSummary TransactionSummary { get; set; }
    }
}