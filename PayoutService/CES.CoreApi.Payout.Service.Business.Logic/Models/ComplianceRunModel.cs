using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class ComplianceRunModel
    {
        public ComplianceRunModel()
        {
            //NOOP:
        }

        public bool WatchListExternal { get; set; } //required
        public bool WatchListPassed { get; set; } //required
        public bool FiltersExternal { get; set; } //required      
        public bool FiltersPassed { get; set; } //required
    }

}
