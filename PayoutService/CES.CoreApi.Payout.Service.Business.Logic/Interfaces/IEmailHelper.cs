using CES.CoreApi.Payout.Service.Business.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Contract.Interfaces
{
    public interface IEmailHelper
    {
        SendEmailResponseModel EnviarMail(SendEmailRequestModel request);
        
    }
}
