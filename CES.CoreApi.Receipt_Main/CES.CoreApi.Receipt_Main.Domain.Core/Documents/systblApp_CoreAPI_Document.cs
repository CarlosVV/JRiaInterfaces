using CES.CoreApi.Receipt_Main.Domain.Core.Activity;
using CES.CoreApi.Receipt_Main.Domain.Core.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class systblApp_CoreAPI_Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public systblApp_CoreAPI_Document()
        {
            DocumentDetails = new HashSet<systblApp_CoreAPI_DocumentDetail>();
            DocumentReferences = new HashSet<systblApp_CoreAPI_DocumentReference>();
            TaskDetails = new HashSet<systblApp_CoreAPI_TaskDetail>();
            Activity = new HashSet<systblApp_TaxReceipt_Activity>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fDocumentId { get; set; }

        [Required]
        [StringLength(20)]
        public string fOrderNo { get; set; }

        [Required]
        [StringLength(10)]
        public string fDocumentType { get; set; }

        public int fFolio { get; set; }

        [StringLength(255)]
        public string fDescription { get; set; }

        [StringLength(50)]
        public string fStoreName { get; set; }

        [StringLength(10)]
        public string fCashRegisterNumber { get; set; }

        [StringLength(100)]
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

        public int fSenderId { get; set; }

        public int fReceiverId { get; set; }

        public bool? fSentToSII { get; set; }

        public bool? fDownloadedSII { get; set; }

        public DateTime fPaymentDate { get; set; }

        public int fRecAgent { get; set; }

        public int? fPayAgent { get; set; }

        public DateTime fTimestampDocument { get; set; }

        public DateTime fTimestampSent { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_CoreAPI_TaxEntity Sender { get; set; }

        public virtual systblApp_CoreAPI_TaxEntity Receiver { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_CoreAPI_DocumentDetail> DocumentDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_CoreAPI_DocumentReference> DocumentReferences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_CoreAPI_TaskDetail> TaskDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_TaxReceipt_Activity> Activity { get; set; }
    }
}
