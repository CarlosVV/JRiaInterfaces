using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxDocumentReference
    {
        public int fTaxDocumentReferenceId { get; set; }

        public int fDocumentId { get; set; }

        public int fLineNumber { get; set; }
     
        public string fDocRefFolio { get; set; }
   
        public string fDocRefType { get; set; }

        public DateTime? fDocRefDate { get; set; }
      
        public string fCodeRef { get; set; }
     
        public string fReasonRef { get; set; }     
    }
}
