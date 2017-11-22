using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class actblTaxDocument_Entity_Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fAddressID { get; set; }

        public int fEntityID { get; set; }

        [Required]
        [StringLength(150)]
        public string fAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string fCounty { get; set; }

        [Required]
        [StringLength(50)]
        public string fCity { get; set; }

        [Required]
        [StringLength(50)]
        public string fState { get; set; }

        public int fCountryId { get; set; }

        public bool fDisabled { get; set; }

        public bool fDelete { get; set; }

        public bool fChanged { get; set; }

        public DateTime fTime { get; set; }

        public DateTime fModified { get; set; }

        public int fModifiedID { get; set; }

        public virtual actblTaxDocument_Entity TaxEntity { get; set; }
    }
}
