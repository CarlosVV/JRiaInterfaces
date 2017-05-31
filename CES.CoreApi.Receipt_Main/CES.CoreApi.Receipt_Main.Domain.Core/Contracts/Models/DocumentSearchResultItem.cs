using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Contracts.Models
{
    public class DocumentSearchResultItem
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Item { get; set; }
        public string Type { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverMiddleName { get; set; }
        public string ReceiverLastName1 { get; set; }
        public string ReceiverLastName2 { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderMiddleName { get; set; }
        public string SenderLastName1 { get; set; }
        public string SenderLastName2 { get; set; }
        public int Folio { get; set; }
        public string Branch { get; set; }
        public string TellerNumber { get; set; }
        public string TellerName { get; set; }
        public string Issued { get; set; }
        public decimal TotalAmount { get; set; }
    }
}