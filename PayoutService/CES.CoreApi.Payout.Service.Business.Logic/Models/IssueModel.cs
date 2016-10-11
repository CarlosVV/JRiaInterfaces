using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class IssueModel
    {
        public int LogID { get; set; }
        public int ItemID { get; set; }
        public int IssueTypeID { get; set; }
        public int IssueItemID { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime ReviewedDate { get; set;}
        public string IssueType { get; set; }
        public string ReviewedByName { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string PermVal { get; set; }
        public int FilterType { get; set; }
        public int StatusID { get; set; }
        public string  IssueCat { get; set; }



    }
}
