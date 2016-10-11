using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Utilities
{
    class Constants
    {

        //Payout Service Codes:
        public const int PAYOUT_SERVICE_SUCCESS_CODE = 0;
        public const string PAYOUT_SERVICE_SUCCESS_MESSAGE = "SUCCESS";
        public const int PAYOUT_SERVICE_FAILURE_CODE = 9010;
        public const string PAYOUT_SERVICE_FAILURE_MESSAGE = "Payout Service: ";

        //Provider Network Codes (Includes the Ria Provider as well):
        public const int PGN_SERVICE_SUCCESS_CODE = 0;
        public const string PNG_SERVICE_SUCCESS_MESSAGE = "SUCCESS";
        public const int PNG_SERVICE_FAILURE_CODE = 9019;
        public const string PNG_SERVICE_FAILURE_MESSAGE = "Provider: ";

    }
}
