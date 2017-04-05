using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxGenerateRequest
    {
        public HeaderInfo HeaderInfo { get; internal set; }
        public long PersistenceID { get; internal set; }
    }
}