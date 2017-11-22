using CES.CoreApi.Receipt_Main.Domain.Core.Activity;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class actblTaxDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public actblTaxDocument()
        {
            DocumentDetails = new HashSet<actblTaxDocument_Detail>();
            DocumentReferences = new HashSet<actblTaxDocument_Reference>();
            TaskDetails = new HashSet<systblApp_CoreAPI_Task_Detail>();
            //Activity = new HashSet<systblApp_TaxReceipt_Activity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fDocumentID { get; set; }

        public int fServiceID { get; set; }

        public int fTransactionID { get; set; }

        [Required]
        [StringLength(20)]
        public string fTransactionNo { get; set; }

        public int fDocumentTypeID { get; set; }

        public int fDocumentStatusID { get; set; }

        [Required]
        [StringLength(20)]
        public string fAuthorizationNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string fDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string fStoreName { get; set; }

        [Required]
        [StringLength(10)]
        public string fCashRegisterNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string fCashierName { get; set; }

        public DateTime fIssuedDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? fExemptAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal fAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal fTaxAmount { get; set; }

        [Column(TypeName = "money")]
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

        [StringLength(8000)]
        public string fXmlDocumentContent { get; set; }

        public int fVATID { get; set; }

        public bool fDisabled { get; set; }

        public bool fDelete { get; set; }

        public bool fChanged { get; set; }

        public DateTime fTime { get; set; }

        public DateTime fModified { get; set; }

        public int fModifiedID { get; set; }

        public virtual actblTaxDocument_Entity EntityFrom { get; set; }

        public virtual actblTaxDocument_Entity EntityTo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actblTaxDocument_Detail> DocumentDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actblTaxDocument_Reference> DocumentReferences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_CoreAPI_Task_Detail> TaskDetails { get; set; }
    }
}
