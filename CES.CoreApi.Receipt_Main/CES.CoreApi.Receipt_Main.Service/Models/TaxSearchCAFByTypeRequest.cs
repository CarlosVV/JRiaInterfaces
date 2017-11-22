using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxSearchCAFByTypeRequest : BaseRequestModel
    {
        public int Id { get; set; }
        public int? RecAgentID { get; set; }
        public int? RecAgentLocID { get; set; }
        public int? DocumentTypeID { get; set; }
        public int? CurrentNumber { get; set; }
        public int? StartNumber { get; set; }
        public int? EndNumber { get; set; }
        public DateTime? DateAuthorizationStart { get; set; }
        public DateTime? DateAuthorizationEnd { get; set; }
    }
}