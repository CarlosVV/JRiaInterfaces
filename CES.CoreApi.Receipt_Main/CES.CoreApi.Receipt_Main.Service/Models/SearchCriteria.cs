using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class SearchCriteria
    {
        public DocumentType DocumentType { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Folio { get; set; }
        public string OrderNo { get; set; }
        public string StoreId { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string Emisor { get; set; }
        public string Receptor { get; set; }
    }
}