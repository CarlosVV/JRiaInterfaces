using System;
using System.Runtime.Serialization;
using CES.CoreApi.Accounting.Service.Contract.Constants;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.AccountingServiceDataContractNamespace)]
    public class GetTransactionSummaryRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int AgentId { get; set; }

        [DataMember]
        public int LocationId { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public bool IsCancelled { get; set; }

        [DataMember]
        public bool IsVoided { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public bool IsReceivingAgent { get; set; }
    }
}