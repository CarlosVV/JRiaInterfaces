using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Models.DTOs
{
    public class Document
    {
        public string DocumentId { get; set; }
        public string OrderNumber { get; set; }
        public string DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentTypeId { get; set; }
        public int Folio { get; set; }        
        public string Branch { get; set; }
        public string TellerNumber { get; set; }
        public string TellerName{ get; set; }
        public DateTime Issued { get; set; }
        public decimal TotalAmount { get; set; }
        public Subject Sender { get; set; }
        public Subject Receiver { get; set; }      
        public IEnumerable<DocumentDetail> DocumentDetails { get; set; }
        public int ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string SenderId { get; internal set; }
        public object ReceiverId { get; internal set; }
    }
}