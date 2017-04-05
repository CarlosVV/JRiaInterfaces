using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
    }
}