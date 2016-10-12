using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.BankAccount.Api.Models
{
    public class CPaConn
    {
        public int PayAgentID { get; set; }
        [DataMember(Name = "Host")]
        public string fHost { get; set; }
        [DataMember(Name = "LogonName")]
        public string fLogonName { get; set; }
        [DataMember(Name = "LogonPwd")]
        public string fLogonPassword { get; set; }
        [DataMember(Name = "AgentCode")]
        public string fAgentCode { get; set; }
        public int WsTimeOutMs { get; set; }

        public CPaConn()
        {
        }
        public CPaConn(int PayagentId)
        {
            PayAgentID = PayagentId;
            fHost = "";
            fLogonName = "";
            fLogonPassword = "";
            fAgentCode = "";
            WsTimeOutMs = 0;
        }
    }
}
