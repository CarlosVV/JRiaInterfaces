using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Shared.Persistence.Model
{
    public class RequesterInfoModel:BaseModel
    {
        public RequesterInfoModel()
        {
                    
        }

        public int RequesterInfoID { get; set; }
        public int AppID { get; set; }
        public int AppObjectID { get; set; }
        public int AgentID { get; set; }
        public int LocAgentID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string TerminalUserID { get; set; }
        public DateTime LocalTime { get; set; }
        public DateTime UtcTime { get; set; }
        public string TimeZone { get; set; }
        public string Locale { get; set; }
        public string Version { get; set; }




    }
}
