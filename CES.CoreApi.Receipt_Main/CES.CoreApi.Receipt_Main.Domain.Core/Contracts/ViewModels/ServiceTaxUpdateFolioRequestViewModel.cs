using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels
{
    public class ServiceTaxUpdateFolioRequestViewModel
    {
        public string Id { get; set; }
        public string DocumentType { get; set; }
        public int? NextFolioNumber { get; set; }
    }
}