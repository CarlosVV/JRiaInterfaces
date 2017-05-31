using CES.CoreApi.Receipt_Main.Service.Models;
using CES.CoreApi.Receipt_Main.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.Receipt_Main.Service.Utilities
{
    static class CodeErrorManager
    {
        public const string CODE_TYPE = "EC";
        public static ResponseCode GetResponseCode(int errorCode, int providerID = 0)
        {
            return GetResponseCodeFromDatabase(errorCode, providerID);
        }

        private static ResponseCode GetResponseCodeFromDatabase(int errorCode, int providerID = 0)
        {

            var coreAPICode = string.Empty;
            var coreAPIMesssage = string.Empty;
            var _codesRepository = new CodesRepositoryCached();
            var coreApiCodes = _codesRepository.GetCoreApiCodesFromProviderByType(providerID, CODE_TYPE);

            errorCode = (errorCode == -1 ? 11 : errorCode);

            if (errorCode == 0)
            {
                return new ResponseCode(998, "Not mapped code");
            }

            var coreApiCode = coreApiCodes.ToList().FirstOrDefault(c => c.fProviderCode == errorCode.ToString());
            if (coreApiCode == null)
            {
                return new ResponseCode(998, "Not mapped code");
            }

            coreAPICode = coreApiCode.fCoreApiCode;
            coreAPIMesssage = coreApiCode.fCoreApiMessage;

            var numCode = 0;
            if (int.TryParse(coreAPICode, out numCode))
            {
                return new ResponseCode(numCode, coreAPIMesssage);
            }


            return new ResponseCode(998, "Not mapped code");

        }
    }
}