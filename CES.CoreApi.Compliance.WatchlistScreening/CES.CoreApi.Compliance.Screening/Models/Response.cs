using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models
{
    public class Response
    {
        public int Code;

        public string Message;

        public IEnumerable<Hit> Hits;

        public bool LegalHold;

        public Rule RuleWasApply;

        public string StatusActimize { get; set; }

        public Response()
        {
           
        }
      

        public Response(int code, string message, IEnumerable<Hit> hits, bool legalHold, Rule ruleWasApply,string statusActimize)
        {
            Code = code;
            Message = message;
            Hits = hits;
            LegalHold = legalHold;
            RuleWasApply = ruleWasApply;
            StatusActimize = statusActimize;


        }
    
    }
}