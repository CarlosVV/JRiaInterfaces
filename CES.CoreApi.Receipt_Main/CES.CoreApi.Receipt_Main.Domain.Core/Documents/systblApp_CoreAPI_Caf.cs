using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{ 
   public partial class systblApp_CoreAPI_Caf
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string CompanyRUT { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyLegalName { get; set; }

        [Required]
        [StringLength(10)]
        public string DocumentType { get; set; }

        public int RecAgent { get; set; }

        public int FolioCurrentNumber { get; set; }

        public int FolioStartNumber { get; set; }

        public int FolioEndNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime AuthorizationDate { get; set; }

        [Required]
        public string FileContent { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
