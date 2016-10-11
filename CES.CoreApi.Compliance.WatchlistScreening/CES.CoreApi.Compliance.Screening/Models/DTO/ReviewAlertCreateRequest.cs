using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models.DTO
{
    public class ReviewAlertCreateRequest
    {
        public long ReviewID { get; set; }
        public DateTime DateTime { get; set; }
        public int ServiceID { get; set; }
        public long TransactionID { get; set; }
        public int IssueID { get; set; }
        public string IssueDescription { get; set; }
        public int? FilterID { get; set; }
        public int IssueTypeID { get; set; }
        public int? ActionID { get; set; }
        public int ProviderID { get; set; }
        public string ProviderAlertID { get; set; }
        public string ProviderAlertStatusID { get; set; }
    }
}