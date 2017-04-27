namespace CES.CoreApi.Receipt_Main.Model.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("systblApp_CoreAPI_Item")]
    public partial class Item
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal? Value { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
