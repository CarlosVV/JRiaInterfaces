using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Interfaces
{
    public interface IRequesterInfo
    {
   
        int AppID { get; set; }
        int AppObjectID { get; set; }
        int AgentID { get; set; }
        int AgentLocID { get; set; }
        int UserID { get; set; }
        string TerminalID { get; set; }
        string TerminalUserID { get; set; }
        DateTime LocalTime { get; set; }
        DateTime UtcTime { get; set; }
        string Timezone { get; set; }
        string Locale { get; set; }
        string Version { get; set; }
    }
}
