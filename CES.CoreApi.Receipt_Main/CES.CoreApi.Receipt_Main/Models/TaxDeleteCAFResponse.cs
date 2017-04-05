﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxDeleteCAFResponse
    {
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
    }
}