using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Model.Documents
{

    [Table("systblApp_CoreAPI_Caf")]
    public partial class Caf
    {
        [Key]
        public Guid? Id { get; set; }

        [StringLength(1000)]
        public string CompanyTaxId { get; set; }

        [StringLength(1000)]
        public string CompanyLegalName { get; set; }

        public int? DocumentType { get; set; }

        public int? FolioCurrentNumber { get; set; }

        public int? FolioStartNumber { get; set; }

        public int? FolioEndNumber { get; set; }

        [StringLength(10)]
        public string DateAuthorization { get; set; }

        public string FileContent { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
