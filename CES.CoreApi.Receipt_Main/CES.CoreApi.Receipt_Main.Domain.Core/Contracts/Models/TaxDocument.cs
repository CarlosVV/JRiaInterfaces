using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxDocument
    {
        public TaxDocument()
        {
            DocumentDetails = new HashSet<TaxDocumentDetail>();
            DocumentReferences = new HashSet<TaxDocumentReference>();
        }

        public int Id { get; set; }

        public string OrderNo { get; set; }

        public string DocumentType { get; set; }

        public int Folio { get; set; }

        public string Description { get; set; }

        public string StoreName { get; set; }

        public string CashRegisterNumber { get; set; }

        public string CashierName { get; set; }

        public DateTime IssuedDate { get; set; }

        public decimal? ExemptAmount { get; set; }

        public decimal Amount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public bool? SentToSII { get; set; }

        public bool? DownloadedSII { get; set; }

        public DateTime PaymentDate { get; set; }

        public int RecAgent { get; set; }

        public int? PayAgent { get; set; }

        public DateTime TimestampDocument { get; set; }

        public DateTime TimestampSent { get; set; }

        public TaxEntity Sender { get; set; }

        public TaxEntity Receiver { get; set; }

        public ICollection<TaxDocumentDetail> DocumentDetails { get; set; }

        public ICollection<TaxDocumentReference> DocumentReferences { get; set; }

    }
}
