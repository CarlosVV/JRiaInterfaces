namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActivityDetail")]
    public partial class ActivityDetail
    {
        public Guid Id { get; set; }

        public Guid? ActivityId { get; set; }

        public Guid? ActivityTypeId { get; set; }

        public string Notes { get; set; }

        [StringLength(60)]
        public string ResultType { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual ActivityType ActivityType { get; set; }
    }
}
