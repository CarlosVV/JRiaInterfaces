using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.ViewModels
{
    public class ServiceTaxCreateDocumentRequestViewModel
    {
        public ServiceTaxCreateDocumentRequestViewModel()
        {
            this.Document = new TaxDocument();
        }       
        public TaxDocument Document { get; set; }
    }
}