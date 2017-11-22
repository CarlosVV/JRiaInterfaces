using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{ 
   public partial class actblTaxDocument_AuthCode
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fAuthCodeID { get; set; }

        public int fCountryID { get; set; }

        [Required]
        [StringLength(20)]
        public string fCompanyTaxID { get; set; }

        [Required]
        [StringLength(50)]
        public string fCompanyLegalName { get; set; }

        public int fDocumentTypeID { get; set; }

        public int fRecAgentID { get; set; }

        public int fRecAgentLocID { get; set; }

        [Required]
        [StringLength(20)]
        public string fCurrentNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string fStartNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string fEndNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime fAuthorizationDate { get; set; }

        [StringLength(8000)]
        public string fFileContent { get; set; }

        public bool fDisabled { get; set; }

        public bool fDelete { get; set; }

        public bool fChanged { get; set; }

        public DateTime fTime { get; set; }

        public DateTime fModified { get; set; }

        public int fModifiedID { get; set; }
    }
}
