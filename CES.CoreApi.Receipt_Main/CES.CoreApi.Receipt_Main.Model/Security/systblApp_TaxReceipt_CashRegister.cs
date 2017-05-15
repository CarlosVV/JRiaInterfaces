namespace CES.CoreApi.Receipt_Main.Model.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class systblApp_TaxReceipt_CashRegister
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? PrinterId { get; set; }

        [StringLength(30)]
        public string PcName { get; set; }

        [StringLength(80)]
        public string Location { get; set; }

        public int? StoreId { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_TaxReceipt_Printer Printer { get; set; }

        public virtual systblApp_TaxReceipt_Store Store { get; set; }
    }
}
