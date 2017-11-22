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

        public int fDocumentID { get; set; }

        public int fServiceID { get; set; }

        public int fTransactionID { get; set; }
        public string fTransactionNo { get; set; }

        public int fDocumentTypeID { get; set; }

        public int fDocumentStatusID { get; set; }

        public string fAuthorizationNumber { get; set; }

        public string fDescription { get; set; }

        public string fStoreName { get; set; }

        public string fCashRegisterNumber { get; set; }

        public string fCashierName { get; set; }

        public DateTime fIssuedDate { get; set; }

        public decimal? fExemptAmount { get; set; }

        public decimal fAmount { get; set; }

        public decimal fTaxAmount { get; set; }

        public decimal fTotalAmount { get; set; }

        public int fEntityFromID { get; set; }

        public int fEntityToID { get; set; }

        public bool fSentToIRS { get; set; }

        public DateTime fSentTime { get; set; }

        public bool fDownloadedIRS { get; set; }

        public DateTime fPaymentDate { get; set; }

        public int fRecAgentID { get; set; }

        public int fPayAgentID { get; set; }

        public DateTime fTimeStampDocument { get; set; }

        public DateTime fTimeStampSent { get; set; }

        public string fXmlDocumentContent { get; set; }

        public int fVATID { get; set; }

        public bool fDisabled { get; set; }

        public bool fDelete { get; set; }

        public bool fChanged { get; set; }

        public DateTime fTime { get; set; }

        public DateTime fModified { get; set; }

        public int fModifiedID { get; set; }

        public virtual TaxEntity EntityFrom { get; set; }

        public virtual TaxEntity EntityTo { get; set; }

        public ICollection<TaxDocumentDetail> DocumentDetails { get; set; }

        public ICollection<TaxDocumentReference> DocumentReferences { get; set; }

    }
}
