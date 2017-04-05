using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Utilities
{
    public class HeaderHelper
    {
        public static string ApplicationId { get; set; }
       
        public static string CesAppObjectId { get; set; }
       
        public static string CesUserId { get; set; }      

        public static string CesRequestTime { get; set; }        
    }
}