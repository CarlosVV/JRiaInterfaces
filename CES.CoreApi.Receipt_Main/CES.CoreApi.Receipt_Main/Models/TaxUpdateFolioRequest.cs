using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxUpdateFolioRequest
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public int? NextFolioNumber { get; set; }
    }
}