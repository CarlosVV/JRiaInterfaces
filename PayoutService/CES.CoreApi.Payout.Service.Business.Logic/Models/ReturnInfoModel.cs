using CES.CoreApi.Payout.Service.Business.Contract.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CES.CoreApi.Payout.Service.Business.Contract.Models
{
    public class ReturnInfoModel
    {
        public ReturnInfoModel()
        {

        }
        public ReturnInfoModel(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool AvailableForPayout { get; set; }
        public string AllowUnusualOrderReporting { get; set; }
        public string RemainingBalanceWarningMsg { get; set; }
        public bool UsePayoutGateway { get; set; }
    }
}
