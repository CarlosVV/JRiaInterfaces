using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Model.CafApiService
{
    public class ServiceTaxCreateCAFResponseViewModel
    {
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }      
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
        public systblApp_CoreAPI_Caf CAF { get; set; }
        public bool ProcessResult { get; set; }
        public long PersistenceId { get; internal set; }
    }
}