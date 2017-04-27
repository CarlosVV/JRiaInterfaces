using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using CES.CoreApi.Receipt_Main.Models.DTOs;

namespace CES.CoreApi.Receipt_Main.ViewModels
{
    public class ServiceTaxGenerateReceiptResponseViewModel
    {
        public Document Document { get; set; }
        public byte[] PDF { get; set; }
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
    }
}