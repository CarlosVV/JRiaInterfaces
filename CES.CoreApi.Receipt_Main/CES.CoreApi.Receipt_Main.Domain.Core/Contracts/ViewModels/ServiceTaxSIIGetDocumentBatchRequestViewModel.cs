using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels
{
    public class ServiceTaxSIIGetDocumentBatchRequestViewModel
    {
        public int FolioStart { get; set; }
        public int FolioEnd { get; set; }
    }
}