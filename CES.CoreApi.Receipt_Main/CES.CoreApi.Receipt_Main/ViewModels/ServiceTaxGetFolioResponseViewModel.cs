﻿using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.ViewModels
{
    public class ServiceTaxGetFolioResponseViewModel
    {
        public CAF CAF { get; set; }
        public int NextFolioNumber { get; set; }
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
    }
}