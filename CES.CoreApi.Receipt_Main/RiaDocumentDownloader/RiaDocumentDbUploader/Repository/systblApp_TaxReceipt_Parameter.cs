namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_TaxReceipt_Parameter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(80)]
        public string Description { get; set; }

        public string Value { get; set; }

        [StringLength(10)]
        public string Type { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_TaxReceipt_ParameterCategory ParameterCategory { get; set; }
    }
}
