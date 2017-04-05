using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Filters.Responses
{
    public class Error
    {
        public int Code { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
    }
}