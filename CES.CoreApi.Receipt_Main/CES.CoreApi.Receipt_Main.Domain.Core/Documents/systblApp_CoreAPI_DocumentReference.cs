using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class systblApp_CoreAPI_DocumentReference
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fDocumentReferenceId { get; set; }

        public int fDocumentId { get; set; }

        public int fLineNumber { get; set; }

        [StringLength(10)]
        public string fDocRefFolio { get; set; }

        [StringLength(10)]
        public string fDocRefType { get; set; }

        public DateTime? fDocRefDate { get; set; }

        [StringLength(10)]
        public string fCodeRef { get; set; }

        [StringLength(100)]
        public string fReasonRef { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_CoreAPI_Document Document { get; set; }
    }
}
