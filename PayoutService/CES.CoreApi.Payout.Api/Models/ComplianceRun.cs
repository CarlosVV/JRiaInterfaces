using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Models
{
    public class ComplianceRun
    {

        [DataMember(EmitDefaultValue = false, IsRequired = true)]
        public bool WatchListExternal { get; set; } //required
        [DataMember(EmitDefaultValue = false, IsRequired = true)]
        public bool WatchListPassed { get; set; } //required
        [DataMember(EmitDefaultValue = false, IsRequired = true)]
        public bool FiltersExternal { get; set; } //required     
        [DataMember(EmitDefaultValue = false, IsRequired = true)]
        public bool FiltersPassed { get; set; } //required


    }
}
