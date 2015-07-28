using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;
using CES.CoreApi.MtTransaction.Service.Contract.Enumerations;

namespace CES.CoreApi.MtTransaction.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.MtTransactionServiceDataContractNamespace)]
    public class MtTransactionGetRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int TransactionId { get; set; }

        [DataMember]
        public int DatabaseId { get; set; }

        [DataMember(IsRequired = true)]
        public TransactionInformationGroup DetalizationLevel { get; set; }
    }
}