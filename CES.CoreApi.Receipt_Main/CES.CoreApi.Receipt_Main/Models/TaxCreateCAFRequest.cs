using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxCreateCAFRequest
    {
        public string CAFContent { get; set; }
        public HeaderInfo HeaderInfo { get; internal set; }
    }
}