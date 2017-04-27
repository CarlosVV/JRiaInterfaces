namespace CES.CoreApi.Receipt_Main.Model.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("systblApp_CoreAPI_TaxAddress")]
    public partial class TaxAddress
    {
        public Guid Id { get; set; }

        public Guid? Entity { get; set; }

        [StringLength(150)]
        public string Line1 { get; set; }

        [StringLength(150)]
        public string Line2 { get; set; }

        [StringLength(150)]
        public string State { get; set; }

        [StringLength(150)]
        public string Country { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
