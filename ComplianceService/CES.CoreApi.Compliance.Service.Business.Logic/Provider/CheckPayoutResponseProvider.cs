using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Business.Logic.Provider
{
    public class CheckPayoutResponseProvider : ICheckPayoutResponseProvider
    {
        private ICheckPayoutProvider _checkPayoutProvider;

        public CheckPayoutResponseProvider(ICheckPayoutProvider checkPayoutProvider)
        {
            _checkPayoutProvider = checkPayoutProvider;
        }
        public CheckPayoutResponseModel CheckPayout(CheckPayoutRequestModel request)
        {
            return _checkPayoutProvider.CheckPayOut();
        }
    }
}
