﻿using CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models;
using CES.CoreApi.Receipt_Main.Domain.Core.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxGetFolioResponse
    {
        public actblTaxDocument_AuthCode CAF { get; set; }
        public int NextFolioNumber { get; set; }
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
    }
}