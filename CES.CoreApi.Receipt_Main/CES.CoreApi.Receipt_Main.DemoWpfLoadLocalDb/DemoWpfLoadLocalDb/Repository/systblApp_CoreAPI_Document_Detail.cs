namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_CoreAPI_Document_Detail
    {
        public Guid Id { get; set; }

        public int LineNumber { get; set; }

        public Guid DocumentId { get; set; }

        [StringLength(50)]
        public string DocRefFolio { get; set; }

        [StringLength(50)]
        public string DocRefType { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? CountProd { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
