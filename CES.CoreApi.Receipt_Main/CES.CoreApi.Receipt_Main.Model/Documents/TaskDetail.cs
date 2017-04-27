namespace CES.CoreApi.Receipt_Main.Model.Documents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("systblApp_CoreAPI_TaskDetail")]
    public partial class TaskDetail
    {
        public Guid Id { get; set; }

        public Guid? Task { get; set; }

        public string StateObject { get; set; }

        public string ResultObject { get; set; }

        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
