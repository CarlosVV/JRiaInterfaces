﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxSIIGetDocumentRequest : BaseRequestModel
    {
        public int Folio { get; set; }     
    }
}