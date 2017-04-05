using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class HeaderInfo
    {
        public string ApplicationId { get; set; }
        public string CesAppObjectId { get; set; }
        public string CesUserId { get; set; }
        public string CesRequestTime { get; set; }
    }
}