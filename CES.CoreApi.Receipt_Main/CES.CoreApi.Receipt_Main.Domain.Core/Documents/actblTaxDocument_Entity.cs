using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class actblTaxDocument_Entity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public actblTaxDocument_Entity()
        {
            EntityFroms = new HashSet<actblTaxDocument>();
            EntityTos = new HashSet<actblTaxDocument>();
            TaxAddresses = new HashSet<actblTaxDocument_Entity_Address>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fEntityID { get; set; }

        public int fCountryID { get; set; }

        [Required]
        [StringLength(20)]
        public string fTaxID { get; set; }

        public int fEntityTypeID { get; set; }

        [StringLength(50)]
        public string fCompanyLegalName { get; set; }

        [Required]
        [StringLength(50)]
        public string fFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string fMiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string fLastName1 { get; set; }

        [Required]
        [StringLength(50)]
        public string fLastName2 { get; set; }

        [Required]
        [StringLength(200)]
        public string fFullName { get; set; }

        [StringLength(2)]
        public string fGender { get; set; }

        [StringLength(200)]
        public string fOccupation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fDateOfBirth { get; set; }

        [StringLength(2)]
        public string fNationality { get; set; }

        [StringLength(2)]
        public string fCountryOfBirth { get; set; }

        [StringLength(30)]
        public string fPhone { get; set; }

        [StringLength(30)]
        public string fCellPhone { get; set; }

        [StringLength(100)]
        public string fEmail { get; set; }

        [StringLength(150)]
        public string fLineOfBusiness { get; set; }

        public int? fEconomicActivity { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actblTaxDocument> EntityFroms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actblTaxDocument> EntityTos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<actblTaxDocument_Entity_Address> TaxAddresses { get; set; }
    }
}
