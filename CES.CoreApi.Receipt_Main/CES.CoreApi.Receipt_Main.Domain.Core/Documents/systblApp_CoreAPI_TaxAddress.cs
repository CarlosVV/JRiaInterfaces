using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class systblApp_CoreAPI_TaxAddress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fTaxAddressId { get; set; }

        public int? fTaxEntityId { get; set; }

        [StringLength(150)]
        public string fAddress { get; set; }

        [StringLength(50)]
        public string fComuna { get; set; }

        [StringLength(50)]
        public string fCity { get; set; }

        [StringLength(50)]
        public string fState { get; set; }

        [StringLength(20)]
        public string fCountry { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_CoreAPI_TaxEntity TaxEntity { get; set; }
    }
}
