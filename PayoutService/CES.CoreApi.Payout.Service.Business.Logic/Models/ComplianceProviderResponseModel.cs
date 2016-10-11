using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class ComplianceProviderResponseModel
    {
        public bool UseRiaCompliance { get; set; }
        public bool UseActimizeCompliance { get; set; }

    }
}
