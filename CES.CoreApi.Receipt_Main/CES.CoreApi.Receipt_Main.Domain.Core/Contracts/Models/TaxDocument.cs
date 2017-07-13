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

        public int fDocumentId { get; set; }

        public string fOrderNo { get; set; }

        public string fDocumentType { get; set; }

        public int fFolio { get; set; }

        public string fDescription { get; set; }

        public string fStoreName { get; set; }

        public string fCashRegisterNumber { get; set; }

        public string fCashierName { get; set; }

        public DateTime fIssuedDate { get; set; }

        public decimal? fExemptAmount { get; set; }

        public decimal fAmount { get; set; }

        public decimal fTaxAmount { get; set; }

        public decimal fTotalAmount { get; set; }

        public int fSenderId { get; set; }

        public int fReceiverId { get; set; }

        public bool? fSentToSII { get; set; }

        public bool? fDownloadedSII { get; set; }

        public DateTime fPaymentDate { get; set; }

        public int fRecAgent { get; set; }

        public int? fPayAgent { get; set; }

        public DateTime fTimestampDocument { get; set; }

        public DateTime fTimestampSent { get; set; }

        public TaxEntity Sender { get; set; }

        public TaxEntity Receiver { get; set; }

        public ICollection<TaxDocumentDetail> DocumentDetails { get; set; }

        public ICollection<TaxDocumentReference> DocumentReferences { get; set; }

    }
}
