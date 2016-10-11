using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class CustomerServiceMessagesModel
    {
        public string MessageID { get; set;}
        public string Category { get; set;}
        public DateTime MessageTime { get; set; }
        public string EnteredBy { get; set; }
        public string MessageBody { get; set; }

    }
}
