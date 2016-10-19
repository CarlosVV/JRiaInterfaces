using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models
{
    public class RulesRequest
    {
        public DateTime TransDateTime { get; set; }
        public int RuntimeID { get; set; }
        public ServiceIdType ServiceId { get; set; }
        public int ProductId { get; set; }
        public int CountryFromId { get; set; }
        public int CountryToId { get; set; }
        public int ReceivingAgentID { get; set; }
        public int ReceivingAgentLocID { get; set; }
        public int PayAgentID { get; set; }
        public int PayAgentLocID { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string EntryType { get; set; }
        public string SendCurrency { get; set; }
        public double SendAmount { get; set; }
        public double SendTotalAmount { get; set; }
        public PartyType PartyType { get; set; }
        public int EntryTypeId { get; set; }
    }
}