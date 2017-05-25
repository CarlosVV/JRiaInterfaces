using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Linq;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Model.CafApiService
{
    public class ReturnInfo
    {
        public ReturnInfo()
        {
            Errors = new List<ErrorModel>();
        }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public Boolean ResultProcess { get; set; }
        public List<ErrorModel> Errors { get; set; }

    }
}