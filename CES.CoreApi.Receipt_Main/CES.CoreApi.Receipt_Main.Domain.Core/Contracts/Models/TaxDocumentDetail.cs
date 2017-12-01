using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxDocumentDetail
    {
        public TaxDocumentDetail()
        {

        }

        public int fDetailID { get; set; }
        public int fDocumentID { get; set; }
        public int fLineNumber { get; set; }
        public string fDocRefFolio { get; set; }
        public string fDocRefType { get; set; }
        public string fDescription { get; set; }
        public int? fItemCount { get; set; }
        public string fCode { get; set; }
        public decimal? fPrice { get; set; }
        public decimal? fAmount { get; set; }
    }
}
