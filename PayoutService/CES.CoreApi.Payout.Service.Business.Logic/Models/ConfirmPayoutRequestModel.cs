using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class ConfirmPayoutRequestModel
    {

        public int PersistenceID { get; set; }

        public RequesterInfoModel RequesterInfo { get; set; }

        public string OrderPIN { get; set; }
        public string IDNumber { get; set; }
        public DateTime IDExpirationDate { get; set; }

    }
}
