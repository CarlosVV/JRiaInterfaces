using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxSIISendDocumentRequest
    {
        public ReturnInfo ReturnInfo { get; internal set; }
    }
}