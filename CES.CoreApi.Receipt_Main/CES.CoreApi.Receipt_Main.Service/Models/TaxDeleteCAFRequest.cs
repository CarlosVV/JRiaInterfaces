﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxDeleteCAFRequest
    {
        public HeaderInfo HeaderInfo { get; internal set; }
        public int Id { get; set; }
    }
}