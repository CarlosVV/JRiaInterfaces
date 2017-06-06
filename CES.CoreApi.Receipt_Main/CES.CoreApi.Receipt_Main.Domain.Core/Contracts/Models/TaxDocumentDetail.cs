using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class TaxDocumentDetail
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public int LineNumber { get; set; }

        public string DocRefFolio { get; set; }

        public string DocRefType { get; set; }

        public string Description { get; set; }

        public int? QtyProd { get; set; }

        public string Code { get; set; }

        public decimal? Price { get; set; }

        public decimal? Amount { get; set; }
    }
}
