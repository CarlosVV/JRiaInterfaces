using System;
using System.Runtime.Serialization;

namespace CES.CoreApi.Logging.Models
{
    [DataContract]
    public class ApplicationContext
    {
        [DataMember]
        public Guid CorrelationId { get; set; }
        [DataMember]
        public string PageId { get; set; }
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public int AgentId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int LoginId { get; set; }
        [DataMember]
        public int CountryId { get; set; }
    }
}
