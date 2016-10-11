using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models
{
    public class Rule
    {
        public int fRuleID { get; set; }
        /// <summary>
        /// (ActionOnHit)
        /// [1]	Reject
        /// [2]	Reject Without Information
        /// [10] On Hold
        /// [11] On Hold Without Information
        /// [20] Log Only
        /// </summary>
        public int ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string ContryFrom { get; set; }
        public string CountryTo { get; set; }      
        public int fActionID { get; set; }
        public PartyType fNameTypeID { get; set; }
        public int fMatchTypeID { get; set; }
        public int fIssueItemID { get; set; }
        public string SearchDef { get; set; }
        public string BusinessUnit { get; set; }

    }
}