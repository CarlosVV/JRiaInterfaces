using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public class systblApp_CoreApi_Sequence
    {
        [Key]
        public int fSequenceId { get; set; }
        public string fEntityName { get; set; }
        public int? fStartId { get; set; }
        public int? fCurrentId { get; set; }
        public bool? fDisabled { get; set; }

        public bool? fDelete { get; set; }

        public bool? fChanged { get; set; }

        public DateTime? fTime { get; set; }

        public DateTime? fModified { get; set; }

        public int? fModifiedID { get; set; }
    }
}
