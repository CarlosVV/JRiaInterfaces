namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_CoreAPI_Document
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20)]
        public string OrderNo { get; set; }

        public int DocumentType { get; set; }

        public int Folio { get; set; }

        [StringLength(100)]
        public string Branch { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string TellerNumber { get; set; }

        [StringLength(100)]
        public string TellerName { get; set; }

        public DateTime Issued { get; set; }

        [Column(TypeName = "money")]
        public decimal? ExemptAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal Tax { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public bool? SentToSII { get; set; }

        public bool? DownloadedSII { get; set; }

        public DateTime PaymentDate { get; set; }

        public int RecAgent { get; set; }

        public int? PayAgent { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
