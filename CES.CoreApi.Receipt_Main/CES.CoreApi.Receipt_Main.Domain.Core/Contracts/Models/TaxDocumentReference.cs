using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxDocumentReference
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public int LineNumber { get; set; }
     
        public string DocRefFolio { get; set; }
   
        public string DocRefType { get; set; }

        public DateTime? DocRefDate { get; set; }
      
        public string CodeRef { get; set; }
     
        public string ReasonRef { get; set; }     
    }
}
