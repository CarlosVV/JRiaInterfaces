using System;
using System.Runtime.Serialization;


namespace CES.CoreApi.Payout.Models
{
    public class RequesterInfo
    {
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public int AppID { get; set; }//required
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 2)]
        public int AppObjectID { get; set; }//required
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 3)]
        public int AgentID { get; set; }//required
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 4)]
        public int AgentLocID { get; set; }//required
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 5)]
        public int UserID { get; set; }//required
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 6)]
        public int UserLoginID { get; set; }//required
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order = 7)]
        public string TerminalID { get; set; }
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order = 8)]
        public string TerminalUserID { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 9)]
        public DateTime LocalTime { get; set; }//required
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order = 10)]
        public DateTime UtcTime { get; set; }
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order =11)]
        public string Timezone { get; set; }
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order = 12)]
        public int TimezoneID { get; set; }
        [DataMember(EmitDefaultValue = false, IsRequired = true, Order = 13)]
        public string Locale { get; set; }//required
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order = 14)]
        public string Version { get; set; }
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order = 15)]
        public string AgentCountry { get; set; }
        [DataMember(EmitDefaultValue = true, IsRequired = false, Order = 16)]
        public string AgentState { get; set; }

    }
}
