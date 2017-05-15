namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_TaxReceipt_FunctionRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public int? FunctionalityId { get; set; }

        public int? MenuId { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_TaxReceipt_Functionality Functionality { get; set; }

        public virtual systblApp_TaxReceipt_Menu Menu { get; set; }

        public virtual systblApp_TaxReceipt_Role Role { get; set; }
    }
}
