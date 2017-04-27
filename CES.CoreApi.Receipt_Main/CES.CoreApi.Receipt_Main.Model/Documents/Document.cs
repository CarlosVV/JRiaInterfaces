namespace CES.CoreApi.Receipt_Main.Model.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("systblApp_CoreAPI_Document")]
    public partial class Document
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string OrderNo { get; set; }

        public Guid? Type { get; set; }

        public int? Folio { get; set; }

        [StringLength(100)]
        public string Branch { get; set; }

        [StringLength(100)]
        public string TellerNumber { get; set; }

        [StringLength(100)]
        public string TellerName { get; set; }

        public DateTime? Issued { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalAmount { get; set; }

        public Guid? SenderId { get; set; }

        public Guid? ReceiverId { get; set; }

        public bool? SentToSII { get; set; }

        public bool? DownloadedSII { get; set; }

        [StringLength(10)]
        public string Date { get; set; }

        [StringLength(10)]
        public string RecAgent { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public TaxEntity Sender { get; set; }
        public TaxEntity Receiver { get; set; }
        public IEnumerable<Document_Detail> DocumentDetails { get; set; }
    }
}
