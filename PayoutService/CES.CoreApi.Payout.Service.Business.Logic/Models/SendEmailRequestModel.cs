using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public  class SendEmailRequestModel
    {
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string MessageFormat { get; set; }
        public string MessageFrom { get; set; }
        public string MessageTo { get; set; }
        public string MessageCc { get; set; }
        public string MessageBcc { get; set; }
        public string MessageSubject { get; set; }
        public string MessageSendMethod { get; set; }
        public int UserNameID { get; set; }
        public string RetVal { get; set; }
        public long MessageID { get; set; }
        public int ProviderID { get; set; }
        public int PersistenceID { get; set; }
    }
}
