using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public partial class systblApp_CoreAPI_TaxEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public systblApp_CoreAPI_TaxEntity()
        {
            Senders = new HashSet<systblApp_CoreAPI_Document>();
            Receivers = new HashSet<systblApp_CoreAPI_Document>();
            TaxAddresses = new HashSet<systblApp_CoreAPI_TaxAddress>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fTaxEntityId { get; set; }

        [Required]
        [StringLength(15)]
        public string fRUT { get; set; }

        [Required]
        [StringLength(50)]
        public string fFirstName { get; set; }

        [StringLength(50)]
        public string fMiddleName { get; set; }

        [StringLength(50)]
        public string fLastName1 { get; set; }

        [StringLength(50)]
        public string fLastName2 { get; set; }

        [StringLength(200)]
        public string fFullName { get; set; }

        [StringLength(2)]
        public string fGender { get; set; }

        [StringLength(200)]
        public string fOccupation { get; set; }

        [Column(TypeName = "Date")]
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
        public virtual ICollection<systblApp_CoreAPI_Document> Senders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_CoreAPI_Document> Receivers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_CoreAPI_TaxAddress> TaxAddresses { get; set; }
    }
}
