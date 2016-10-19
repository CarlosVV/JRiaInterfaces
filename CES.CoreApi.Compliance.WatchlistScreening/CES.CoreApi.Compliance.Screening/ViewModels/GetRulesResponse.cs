using CES.CoreApi.Compliance.Screening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.ViewModels
{
    public class GetRulesResponse
    {
        public List<Rule> Rules { get; set; }
    }
}