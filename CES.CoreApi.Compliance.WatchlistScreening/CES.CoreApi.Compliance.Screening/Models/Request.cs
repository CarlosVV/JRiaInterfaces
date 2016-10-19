using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models
{
    public class Request
    {
        public long OrderId { get; set; }
        public string CountryFrom { get; set; }
        public int CountryFromId { get; set; }
        public string CountryTo { get; set; }
        public int CountryToId { get; set; }
        public string SendCurrency { get; set; }
        public double SendAmount { get; set; }
        public double SendTotalAmount { get; set; }
        public Agent ReceivingAgent { get; set; }
        public Agent PayAgent { get; set; }
        public IEnumerable<Party> Parties { get; set; }
        public int ProductId { get; set; }
        public int RuntimeID { get; set; }
        public DateTime TransDateTime { get; set; }
        public string OrderNo { get; set; }
        public ServiceIdType ServiceId { get; set; }
        public string EntryType { get; set; }
        public int EntryTypeId { get; set; }
        public string TransferReason { get; set; }
        public CallEventType CallEvent { get; set; }
        public string OrderPin { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }


    }
}