using System;

namespace CES.CoreApi.Receipt_Main.Service.Models.DTOs
{
    public class Item
    {
        public string ItemId { get; set;}
        public string ItemDescription { get; set; }
        public string ItemFee { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}