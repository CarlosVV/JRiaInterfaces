using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class LocInfoForExternalLocNResponseModel
    {
        public int RiaInternalLocID { get; set; }
        public bool LocIsDisabled { get; set; }
        public bool LocIsDeleted { get; set; }
        public bool LocIsOnHold { get; set; }
        public bool LocCannotTakeOrders { get; set; }
        public bool LocCannotPayOrders { get; set; }
    }
}
