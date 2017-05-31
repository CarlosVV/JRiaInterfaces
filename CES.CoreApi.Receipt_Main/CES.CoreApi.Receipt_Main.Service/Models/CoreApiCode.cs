using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Receipt_Main.Service.Models
{
    public class CoreApiCode
    {
        public int fAppID { get; set; }
        public int fProviderID { get; set; }
        public string fCodeType { get; set; }
        public string fProviderCode { get; set; }
        public string fCoreApiCode { get; set; }
        public string fCoreApiMessage { get; set; }

    }
}
