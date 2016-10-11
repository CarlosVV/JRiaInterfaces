using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Compliance.Screening.Models
{
    public class SendEmailResponse
    {
        public int ReturnValue { get; set; }
        public long ReturnMessageID { get; set; }
    }
}