using CES.CoreApi.Receipt_Main.Model.Documents;
//using CES.CoreApi.Receipt_Main.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models
{
    public class TaxGenerateReceiptResponse
    {
        public Document Document { get; set; }
        public byte[] PDF { get; set; }
        public DateTime TransferDate { get; set; }
        public ReturnInfo ReturnInfo { get; set; }
        public DateTime ResponseTime { get; set; }
        public DateTime ResponseTimeUTC { get; set; }
    }
}