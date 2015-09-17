using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class MtTransactionCreateRequest : BaseRequest
    {
        public int CustomerId { get; set; }

        public int ReceivingAgentId { get; set; }

        public int ReceivingAgentLocationId { get; set; }

        public int PayingAgentId { get; set; }

        public int PayingAgentLocationId { get; set; }

        public Beneficiary Beneficiary { get; set; }
    }
}