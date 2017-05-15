namespace CES.CoreApi.Receipt_Main.Model.Tasks
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class systblApp_CoreAPI_Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public systblApp_CoreAPI_Task()
        {
            TaskDetail = new HashSet<systblApp_CoreAPI_TaskDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int TaskType { get; set; }

        [StringLength(120)]
        public string Method { get; set; }

        public string RequestObject { get; set; }

        public int? ThreadId { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public DateTime? LastExecutionDateTime { get; set; }

        public int? CountExecution { get; set; }

        public int? Status { get; set; }

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
