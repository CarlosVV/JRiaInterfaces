namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_CoreAPI_DocumentReference
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public int LineNumber { get; set; }

        [StringLength(10)]
        public string DocRefFolio { get; set; }

        [StringLength(10)]
        public string DocRefType { get; set; }

        public DateTime? DocRefDate { get; set; }

        [StringLength(10)]
        public string CodeRef { get; set; }

        [StringLength(100)]
        public string ReasonRef { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_CoreAPI_Document Document { get; set; }
    }
}
