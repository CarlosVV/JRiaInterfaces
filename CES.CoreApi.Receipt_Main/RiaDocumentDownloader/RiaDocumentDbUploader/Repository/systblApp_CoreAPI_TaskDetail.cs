namespace WpfLocalDb.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class systblApp_CoreAPI_TaskDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? TaskId { get; set; }

        public string StateObject { get; set; }

        public string ResultObject { get; set; }

        public int? DocumentId { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }

        public virtual systblApp_CoreAPI_Document Document { get; set; }

        public virtual systblApp_CoreAPI_Task Task { get; set; }
    }
}
