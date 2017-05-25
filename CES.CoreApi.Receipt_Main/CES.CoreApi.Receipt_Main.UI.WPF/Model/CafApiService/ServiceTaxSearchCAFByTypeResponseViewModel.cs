using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Model.CafApiService
{
    public class ServiceTaxSearchCAFByTypeResponseViewModel
    {
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
        public List<systblApp_CoreAPI_Caf> Results;
        public bool ProcessResult { get; set; }
    }
}