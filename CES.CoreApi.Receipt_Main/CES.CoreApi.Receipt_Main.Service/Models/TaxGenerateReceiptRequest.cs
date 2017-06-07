﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxGenerateReceiptRequest
    {
        public string OrderNumber { get; set; }
        public int Folio { get; set; }
        public HeaderInfo HeaderInfo { get; internal set; }
    }
}