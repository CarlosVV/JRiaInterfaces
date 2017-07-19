using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class TaxSearchDocumentRequest : BaseRequestModel
    {
        public string DocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentTypeCode { get; set; }
        public string ItemCode { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverMiddleName { get; set; }
        public string ReceiverLastName1 { get; set; }
        public string ReceiverLastName2 { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderMiddleName { get; set; }
        public string SenderLastName1 { get; set; }
        public string SenderLastName2 { get; set; }
        public int DocumentFolio { get; set; }
        public string DocumentBranch { get; set; }
        public string DocumentTellerNumber { get; set; }
        public string DocumentTellerName { get; set; }
        public DateTime? DocumentIssued { get; set; }
        public decimal? DocumentTotalAmount { get; set; }       
    }
}