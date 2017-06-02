using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxSearchCAFByTypeRequest
    {
        public int Id { get; set; }
        public int? RecAgent { get; set; }
        public string DocumentType { get; set; }
        public int? FolioCurrentNumber { get; set; }
        public int? FolioStartNumber { get; set; }
        public int? FolioEndNumber { get; set; }
        public DateTime? DateAuthorizationStart { get; set; }
        public DateTime? DateAuthorizationEnd { get; set; }
        public HeaderInfo HeaderInfo { get; internal set; }
    }
}