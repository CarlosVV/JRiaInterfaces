using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Model.CafApiService
{
    public class ServiceTaxSearchCAFByTypeRequestViewModel
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