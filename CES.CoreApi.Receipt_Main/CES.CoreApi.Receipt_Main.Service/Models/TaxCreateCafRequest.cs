using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxCreateCafRequest : BaseRequestModel
    {
        public string DocumentType { get; set; }
        public int? RecAgent { get; set; }
        public int? FolioStartNumber { get; set; }
        public int? FolioEndNumber { get; set; }
        public int? FolioCurrentNumber { get; set; }
        public string CAFContent { get; set; }
    }
}