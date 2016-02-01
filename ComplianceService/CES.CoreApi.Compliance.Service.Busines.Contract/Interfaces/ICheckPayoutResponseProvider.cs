using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Compliance.Service.Business.Contract.Interfaces
{
    public interface ICheckPayoutResponseProvider
    {
        CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request);
    }
}
