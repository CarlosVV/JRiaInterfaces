namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_TaxReceipt_Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public systblApp_TaxReceipt_Store()
        {
            CashRegister = new HashSet<systblApp_TaxReceipt_CashRegister>();
            Printer = new HashSet<systblApp_TaxReceipt_Printer>();
            Users = new HashSet<systblApp_TaxReceipt_User>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(80)]
        public string Name { get; set; }

        [StringLength(80)]
        public string Location { get; set; }

        [StringLength(20)]
        public string SiiCode { get; set; }

        [StringLength(50)]
        public string PhoneNumber1 { get; set; }

        [StringLength(50)]
        public string PhoneNumber2 { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_TaxReceipt_CashRegister> CashRegister { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_TaxReceipt_Printer> Printer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_TaxReceipt_User> Users { get; set; }
    }
}
