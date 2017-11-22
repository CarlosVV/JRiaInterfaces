using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Domain.Core.Documents
{
    public class OrderInfoResult
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal OrderTaxAmount { get; set; }
        public decimal OrderCommission { get; set; }
        public decimal TotalAmount { get; set; }
        public int fRecAgentLocID { get; set; }
        public int fConfirmedBy { get; set; }
    }
}
