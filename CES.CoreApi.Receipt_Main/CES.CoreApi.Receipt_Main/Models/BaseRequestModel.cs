using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class BaseRequestModel
    {
        public HeaderInfo HeaderInfo { get; set; }
        public long PersistenceID { get; internal set; }
    }
}