using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels
{
    public class ServiceTaxCreateCAFResponseViewModel
    {
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }      
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
        public actblTaxDocument_AuthCode CAF { get; set; }
        public bool ProcessResult { get; set; }
        public long PersistenceId { get; set; }
    }
}