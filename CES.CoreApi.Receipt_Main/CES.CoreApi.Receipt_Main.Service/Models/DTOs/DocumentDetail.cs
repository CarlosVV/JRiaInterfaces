using System;

namespace CES.CoreApi.Receipt_Main.Service.Models.DTOs
{
    public class DocumentDetail
    {
        public string DocumentId { get; set; }
        public int LineNumber { get; set; }
        public string DocumentDetailId { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
        public decimal Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}