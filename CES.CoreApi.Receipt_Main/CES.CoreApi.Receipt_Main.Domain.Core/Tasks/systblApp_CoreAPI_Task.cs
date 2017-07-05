using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace CES.CoreApi.Receipt_Main.Domain.Core.Tasks
{ 
    public partial class systblApp_CoreAPI_Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public systblApp_CoreAPI_Task()
        {
            TaskDetail = new HashSet<systblApp_CoreAPI_TaskDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fTaskId { get; set; }

        public int fTaskType { get; set; }

        [StringLength(120)]
        public string fMethod { get; set; }

        public string fRequestObject { get; set; }

        public int? fThreadId { get; set; }

        public DateTime? fStartDateTime { get; set; }

        public DateTime? fEndDateTime { get; set; }

        public DateTime? fLastExecutionDateTime { get; set; }

        public int? fCountExecution { get; set; }

        public int? fStatus { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_CoreAPI_TaskDetail> TaskDetail { get; set; }
    }
}
