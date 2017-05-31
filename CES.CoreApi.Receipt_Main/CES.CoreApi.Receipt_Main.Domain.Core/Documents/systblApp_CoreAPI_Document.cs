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
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderNo { get; set; }

        [Required]
        [StringLength(10)]
        public string DocumentType { get; set; }

        public int Folio { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(50)]
        public string StoreName { get; set; }

        [StringLength(10)]
        public string CashRegisterNumber { get; set; }

        [StringLength(100)]
        public string CashierName { get; set; }

        public DateTime IssuedDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? ExemptAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "money")]
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
