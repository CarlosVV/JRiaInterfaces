using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class ProviderInfoModel
    {
        public int ProviderID { get; set; }
        public int ProviderTypeID { get; set; }
        public string ProviderName { get; set; }
    }
}
