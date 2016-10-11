using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Payout.Service.Business.Logic.Providers.Correspondents.GoldenCrown
{

    /// <summary>
    /// Translate a Golden Crown errror code into an error message.
    /// </summary>
    class GC_ErrorCode
    {

        public static string S_GetErrorMessageFromCode(string code)
        {
            string msg = "";
            switch (code)
            {
                case "1001":
                    msg = "Insufficient privileges";
                    break;
                case "1002":
                    msg = "Location is not registered in the system";
                    break;
                case "1003":
                    msg = "Operator is not registered in the system";
                    break;
                case "1005":
                    msg = "Operator is locked";
                    break;
                case "1016":
                    msg = "Large volume data set to reflect. Change selection criteria";
                    break;
                case "1031":
                    msg = "Operation does not correspond to tarriff limit";
                    break;
                case "1036":
                    msg = "City %1 lacks payout locations";
                    break;
                case "1042":
                    msg = "Unknown country";
                    break;
                case "1051":
                    msg = "Agent does not provide service";
                    break;
                case "1054":
                    msg = "Daily payment limit exceeded.";
                    break;
                case "1059":
                    msg = "Insufficient funds in Originating Bank's account for payout";
                    break;
                case "1061":
                    msg = "Sender's location is not registered in the system";
                    break;
                case "1065":
                    msg = "Unknown transfer";
                    break;
                case "1066":
                    msg = "Invalid transfer transaction";
                    break;
                case "1067":
                    msg = "Transfer cannot be paid out prior to the due date";
                    break;
                case "1068":
                    msg = "Transfer expired";
                    break;
                case "1069":
                    msg = "Transfer is being processed by another operator, transfer is not available";
                    break;
                case "1070":
                    msg = "Transfer payout is impossible, advise another location to a client";
                    break;
                case "1072":
                    msg = "Unauthorised access	 	 ";
                    break;
                case "1081":
                    msg = "Impossible to send a transfer. Insufficient funds in the account	 	 ";
                    break;
                case "1087":
                    msg = "Transfer has not passed internal checks, please try again later.	 	 ";
                    break;
                case "1103":
                    msg = "Unknown direction";
                    break;
                case "1117":
                    msg = "Amount of sender's transactions per month exceeds money transfer system limit. Total limit renewal will be 30 days past the date of the latest transaction. We apologize for the troubles caused.";
                    break;
                case "1118":
                    msg = "Amount of receiver's transactions per month exceeds money transfer system limit. Total limit renewal will be 30 days past the date of the latest transaction. We apologize for the troubles caused.";
                    break;
                case "1120":
                    msg = "Maximum transfer amount equal to  %amount% %cur% has been exceeded";
                    break;
                case "1121":
                    msg = "Limit of transfers from resident has been exceeded. Maximum amount equals to %amount% %cur%";
                    break;
                case "1157":
                    msg = "Exchange rate has been changed. Recalculate fee";
                    break;
                case "1160":
                    msg = "Current time is not included into location's operating hours.";
                    break;
                case "1161":
                    msg = "Transferred IP does not correspond to the agent's access settings.";
                    break;
                case "1162":
                    msg = "Location's daily transfer limit has been exceeded.";
                    break;
                case "1173":
                    msg = "Amount of sender's large-sum transactions for 90 days period exceeds money transfer system limit.";
                    break;
                case "1174":
                    msg = "Amount of receiver's large-sum transactions for 90 days period exceeds money transfer system limit.";
                    break;
                case "1199":
                    msg = "Conflicting data on client's residence.";
                    break;
                case "1200":
                    msg = "Residence not established";
                    break;
                case "1202":
                    msg = "Specified person is not indicated in the profile";
                    break;
                case "1206":
                    msg = "Wrong mobile phone format. Acceptable format: +79requestModel.xxxx";
                    break;
                case "1209":
                    msg = "Invalid receiver's mobile phone number";
                    break;
                case "4084":
                    msg = "External system not connected";
                    break;
                case "4085":
                    msg = "External system request error. %1";
                    break;
                case "5001":
                    msg = "Transfer is under validation, please try again later";
                    break;
                case "9999":
                    msg = "Unknown error";
                    break;
                default:
                    msg = "could not translate error message.";
                    break;
            }
            return msg;
        }


    }
}
