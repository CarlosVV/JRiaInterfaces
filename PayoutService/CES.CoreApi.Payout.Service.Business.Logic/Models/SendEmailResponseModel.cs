using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class SendEmailResponseModel
    {
        public int ReturnValue { get; set; }
        public long ReturnMessageID { get; set; }
    }
}
