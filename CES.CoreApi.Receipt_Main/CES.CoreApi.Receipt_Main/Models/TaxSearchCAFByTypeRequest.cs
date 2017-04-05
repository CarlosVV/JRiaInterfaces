using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxSearchCAFByTypeRequest
    {
        public string Id { get; set; }
        public int? DocumentType { get; set; }
        public int? FolioCurrentNumber { get; set; }
        public int? FolioStartNumber { get; set; }
        public int? FolioEndNumber { get; set; }
        public DateTime? DateAuthorizationStart { get; set; }
        public DateTime? DateAuthorizationEnd { get; set; }
    }
}