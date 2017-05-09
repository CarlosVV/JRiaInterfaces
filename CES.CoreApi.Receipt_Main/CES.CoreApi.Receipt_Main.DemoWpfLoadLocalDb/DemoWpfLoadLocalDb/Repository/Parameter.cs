namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Parameter")]
    public partial class Parameter
    {
        public Guid Id { get; set; }

        public Guid? CategoryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(400)]
        public string Description { get; set; }

        public string Value { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual ParameterCategory ParameterCategory { get; set; }
    }
}
