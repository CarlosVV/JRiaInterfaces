using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxUpdateCafRequest
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public int? RecAgent { get; set; }
        public int? FolioStartNumber { get; set; }
        public int? FolioEndNumber { get; set; }
        public string CAFContent { get; set; }
        public int? FolioCurrentNumber { get; internal set; }
        public bool Disabled { get; set; }
        public HeaderInfo HeaderInfo { get; internal set; }
    }
}