namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_CoreAPI_TaxEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(15)]
        public string RUT { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName1 { get; set; }

        [StringLength(50)]
        public string LastName2 { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(2)]
        public string Gender { get; set; }

        [StringLength(200)]
        public string Occupation { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(2)]
        public string Nationality { get; set; }

        [StringLength(2)]
        public string CountryOfBirth { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(30)]
        public string CellPhone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(150)]
        public string LineOfBusiness { get; set; }

        public int? EconomicActivity { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
