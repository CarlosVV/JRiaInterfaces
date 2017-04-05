using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models.DTOs
{
    public class Tax
    {
        public ReturnInfo ReturnInfo { get; internal set; }
        public long PersistenceID { get; internal set; }
    }
}