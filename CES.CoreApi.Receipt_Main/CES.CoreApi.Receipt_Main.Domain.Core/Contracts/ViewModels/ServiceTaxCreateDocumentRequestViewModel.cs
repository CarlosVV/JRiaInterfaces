using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels
{
    public class ServiceTaxCreateDocumentRequestViewModel
    {
        //public systblApp_CoreAPI_Document Document {get; set;}
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public decimal TotalCharges { get; set; }
        public int CashRegisterNumber { get; set; }
        public string CashierName { get; set; }
        public string BranchName { get; set; }
    }
}