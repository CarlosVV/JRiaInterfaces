namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_TaxReceipt_Printer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public systblApp_TaxReceipt_Printer()
        {
            CashRegister = new HashSet<systblApp_TaxReceipt_CashRegister>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int StoreId { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(30)]
        public string IpAddress { get; set; }

        [StringLength(10)]
        public string PrinterType { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_TaxReceipt_CashRegister> CashRegister { get; set; }

        public virtual systblApp_TaxReceipt_Store Store { get; set; }
    }
}
