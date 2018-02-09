using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class actblTaxDocument_Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fDetailID { get; set; }

        public int fDocumentID { get; set; }

        public int fLineNumber { get; set; }

        [StringLength(250)]
        public string fDescription { get; set; }

        public int fItemCount { get; set; }

        [StringLength(50)]
        public string fCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? fPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? fAmount { get; set; }

        public bool fDisabled { get; set; }

        public bool fDelete { get; set; }

        public bool fChanged { get; set; }

        public DateTime fTime { get; set; }

        public DateTime fModified { get; set; }

        public int fModifiedID { get; set; }

        public virtual actblTaxDocument Document { get; set; }
    }
}