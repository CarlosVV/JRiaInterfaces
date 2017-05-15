namespace CES.CoreApi.Receipt_Main.Model.Activity
{
    using Documents;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class systblApp_TaxReceipt_Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public systblApp_TaxReceipt_Activity()
        {
            ActivityDetail = new HashSet<systblApp_TaxReceipt_ActivityDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(10)]
        public string OrderNo { get; set; }

        public int? DocumentId { get; set; }

        [StringLength(10)]
        public string Folio { get; set; }

        public int? StatusId { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_CoreAPI_Document Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<systblApp_TaxReceipt_ActivityDetail> ActivityDetail { get; set; }
    }
}
