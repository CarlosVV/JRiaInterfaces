using System;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Models
{
    public class TransactionStatusModel
    {
        public bool NeedsApproval { get; set; }
        public int EnteredBy { get; set; }
        public int ConfirmedBy { get; set; }
        public DateTime ConfirmedTime { get; set; }
        public int CancelledBy { get; set; }
        public DateTime CancelledTime { get; set; }
        public bool Posted { get; set; }
        public int OnHold { get; set; }
        public bool ReadyForPosting { get; set; }
        public bool LegalHold { get; set; }
        public string EntryType { get; set; }
        public DateTime EnteredTime { get; set; }
    }
}