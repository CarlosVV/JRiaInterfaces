using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models.DTO
{
    public class ReviewAlertCreateResponse
    {
        public string rstResult { get;set;}
        public  int  RetVal { get; set; }
        public string RetMsg { get; set; }
        public long ReviewID { get; set; }
        public long ReviewItemID { get; set; }
        public long reviewIssueID { get; set; }
    }
}