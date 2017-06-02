using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels
{
    public class ServiceTaxSearchCAFByTypeRequestViewModel
    {
        public int Id { get; set; }
        public int? RecAgent { get; set; }
        public string DocumentType { get; set; }
        public int? FolioCurrentNumber { get; set; }
        public int? FolioStartNumber { get; set; }
        public int? FolioEndNumber { get; set; }
        public DateTime? DateAuthorizationStart { get; set; }
        public DateTime? DateAuthorizationEnd { get; set; }
    }
}