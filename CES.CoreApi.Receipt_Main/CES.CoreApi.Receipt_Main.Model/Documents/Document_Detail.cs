namespace CES.CoreApi.Receipt_Main.Model.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("systblApp_CoreAPI_Document")]
    public partial class Document_Detail
    {
        public Guid Id { get; set; }

        public int LineNumber { get; set; }

        public Guid DocumentId { get; set; }

        public Guid ItemId { get; set; }

        [StringLength(50)]
        public string DocRefFolio { get; set; }

        [StringLength(50)]
        public string DocRefType { get; set; }

        public int? Count { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
