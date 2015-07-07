using System;

namespace CES.CoreApi.Accounting.Service.Business.Contract.Models
{
    public class GetTransactionSummaryRequestModel
    {
        public int AgentId { get; set; }

        public int LocationId { get; set; }

        public string Currency { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsVoided { get; set; }

        public DateTime OrderDate { get; set; }

        public bool IsReceivingAgent { get; set; }
    }
}