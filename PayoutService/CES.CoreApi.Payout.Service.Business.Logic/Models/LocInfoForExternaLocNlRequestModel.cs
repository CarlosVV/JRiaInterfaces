using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class LocInfoForExternaLocNlRequestModel
    {
        public int AppID { get; set; }
        public int AppObjectID { get; set; }
        public int UserNameID { get; set; }
        public int CorrespID { get; set; }
        public int RiaLocIDFromClient { get; set; }
        public string ExternalLocNo { get; set; }
        public bool IgnoreDisabledStatus { get; set; }

    }
}
