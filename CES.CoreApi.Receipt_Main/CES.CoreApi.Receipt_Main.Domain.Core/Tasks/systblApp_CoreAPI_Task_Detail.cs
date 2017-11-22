using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Tasks
{
    public partial class systblApp_CoreAPI_Task_Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long fDetailID { get; set; }

        public long? fTaskID { get; set; }

        [StringLength(8000)]
        public string fStateObject { get; set; }

        [StringLength(8000)]
        public string fResultObject { get; set; }

        public int fDocumentID { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual actblTaxDocument Document { get; set; }

        public virtual systblApp_CoreAPI_Task Task { get; set; }
    }
}
