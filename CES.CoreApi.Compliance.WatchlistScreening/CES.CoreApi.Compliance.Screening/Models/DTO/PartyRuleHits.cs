using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models.DTO
{
    public class PartyRuleHits
    {
        public int? Code { get; set; }
        public string Status { get; set; }
        public string AlertId { get; set; }
        public Party Party { get; set; }
        public Rule Rule { get; set; }
        public List<Hit> Hits { get; set; }
    }
}