using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class RequesterInfoModel
    {
        public int AppID { get; set; }//required
        public int AppObjectID { get; set; }//required
        public int AgentID { get; set; }//required
        public int AgentLocID { get; set; }//required
        public int UserID { get; set; }//required
        public int UserLoginID { get; set; }//required
        public string TerminalID { get; set; }
        public string TerminalUserID { get; set; }
        public DateTime LocalTime { get; set; }//required
        public DateTime UtcTime { get; set; }
        public string Timezone { get; set; }
        public int TimezoneID { get; set; }
        public string Locale { get; set; }//required
        public string Version { get; set; }

		public string AgentCountry { get; set; }
		public string AgentState { get; set; }
	}
}
