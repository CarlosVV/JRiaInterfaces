using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class PlaceOnLegalHoldRequestModel
    {
        public int ServiceID { get; set; }
        public long TransactionID { get; set; }
        public string StatusNote { get; set; }
        public int UserID { get; set; }
        public DateTime Modified { get; set; }
        public int AppID { get; set; }
        public int AppObjtectID { get; set; }

    }
}
