using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Business.Contract.Interfaces
{
    public interface ICheckPayoutProvider
    {
        CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request);
    }
}
