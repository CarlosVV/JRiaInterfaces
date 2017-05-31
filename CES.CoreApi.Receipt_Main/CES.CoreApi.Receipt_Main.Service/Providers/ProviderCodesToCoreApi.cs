using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Providers
{
    public static class ProviderCodesToCoreApi
    {
        public const int RIA_PROVIDER = 5005;
        public const int MX_PROVIDER = 5004;
        #region TransferStatusCode
        public static class TransferStatusCode
        {

            public static ResponseCode Translate(string providerTransaferStatusCode, int providerID)
            {
                return null;
            }
        }
        #endregion
        #region ErrorCode
        public static class ErrorCode
        {

            public static ResponseCode TranslateErrorCode(int erroCode, int providerID = 0)
            {
                return CodeErrorManager.GetResponseCode(erroCode, providerID);
            }


        }
        #endregion


    }
}