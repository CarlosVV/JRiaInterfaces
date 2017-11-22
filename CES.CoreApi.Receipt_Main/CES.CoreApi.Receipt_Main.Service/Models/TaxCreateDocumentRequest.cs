using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxCreateDocumentRequest : BaseRequestModel
    {
        public TaxDocument Document { get; set; }
    }
}