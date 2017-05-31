using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Activity
{
    public partial class systblApp_TaxReceipt_ActivityDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? ActivityId { get; set; }

        public int? ActivityTypeId { get; set; }

        public string Notes { get; set; }

        [StringLength(60)]
        public string ResultType { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_TaxReceipt_Activity TaxReceipt_Activity { get; set; }
    }
}
