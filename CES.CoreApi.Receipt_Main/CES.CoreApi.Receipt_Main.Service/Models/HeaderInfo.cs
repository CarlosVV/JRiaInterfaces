using CES.CoreApi.Receipt_Main.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class HeaderInfo
    {
        public string ApplicationId { get; set; }
        public string CesAppObjectId { get; set; }
        public string CesUserId { get; set; }
        public string CesRequestTime { get; set; }

        public HeaderInfo()
        {
            ApplicationId = HeaderHelper.ApplicationId;
            CesUserId = HeaderHelper.CesUserId;
            CesAppObjectId = HeaderHelper.CesAppObjectId;
            CesRequestTime = HeaderHelper.CesRequestTime;
        }
    }
}