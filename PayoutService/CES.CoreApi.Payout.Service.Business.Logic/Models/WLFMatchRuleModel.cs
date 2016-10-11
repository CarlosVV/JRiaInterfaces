using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class WLFMatchRuleModel
    {
        public int ProvideID { get; set; }
        public string ProviderName { get; set; }
        public int RuleID { get; set; }
        public int ActionID { get; set; }
        public int NameTypeID { get; set; }
        public int MatchTypeID { get; set; }
        public int IssueItemID { get; set; }

    }
}
