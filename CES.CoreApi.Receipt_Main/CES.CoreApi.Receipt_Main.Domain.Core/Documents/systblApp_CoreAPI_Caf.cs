using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{ 
   public partial class systblApp_CoreAPI_Caf
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fCafId { get; set; }

        [Required]
        [StringLength(10)]
        public string fCompanyRUT { get; set; }

        [Required]
        [StringLength(50)]
        public string fCompanyLegalName { get; set; }

        [Required]
        [StringLength(10)]
        public string fDocumentType { get; set; }

        public int fRecAgent { get; set; }

        public int fFolioCurrentNumber { get; set; }

        public int fFolioStartNumber { get; set; }

        public int fFolioEndNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime fAuthorizationDate { get; set; }

        [Required]
        [StringLength(8000)]
        public string fFileContent { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
