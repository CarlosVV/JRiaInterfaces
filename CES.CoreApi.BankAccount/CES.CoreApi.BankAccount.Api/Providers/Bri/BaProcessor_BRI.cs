using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.BankAccount.Service.Business.Logic.Processors
{
    class BaProcessor_BRI : BaseBaProcessor
    {//BRI: 18268311
        public override BankAccountInfo ValidateAccount(string sName, string sBankName, string sBankCode, string sAccountNumber)
        {
            
            LogMessage(string.Format("VBA BRI, {0}, {1}, start...", sBankCode, sAccountNumber));
            BankAccountInfo resp = new BankAccountInfo(ProcessorID, sName, sBankCode, sBankName, sAccountNumber);

            CPaConn paConn = GetPaConn();

            string sRegisteredIP = TextUtil.GetStr(PaSetting["RegisteredIP"]);

            if (paConn.fHost == "" || paConn.fLogonName == "" || paConn.fLogonPassword == "" || paConn.fAgentCode == "" || sRegisteredIP == "")
            {
                resp.ErrorDesc = "Host/LogonName/LogonPwd/AgentCode/RegisteredIP setting missing;";
                resp.ErrorCode = 2001;
                return resp;
            }

            int nExportFormatID;
            int.TryParse(TextUtil.GetStr(PaSetting["ExportFormatID"]), out nExportFormatID);

            BriWs briWs = new BriWs(paConn.fHost, paConn.fAgentCode, paConn.fLogonName, paConn.fLogonPassword, sRegisteredIP, paConn.WsTimeOutMs);
            briWs.BaProcessor = this;

            string sPAMessage, sErr;
            WS_BRI.inquiryAccount_CT_Result wsResp = briWs.InquiryAccount2(sBankCode, sAccountNumber, out sPAMessage, out sErr);

            LogMessage(string.Format("VBA BRI, {0}, {1}", sPAMessage, sErr));

            if (wsResp != null)
            {
                if (!isWsErrorCodePresent(wsResp.statusCode)) resp.IsAccountValid = isWsOKCodePresent(wsResp.statusCode);
                resp.fBankAcctHolderName = wsResp.accountName;
                if (!(resp.IsAccountValid.HasValue && resp.IsAccountValid.Value))
                {
                    resp.fStatusCode = wsResp.statusCode;
                    resp.fStatusMsg = wsResp.message;
                }
                else resp.fStatusMsg = sPAMessage;
            }
            else if (!string.IsNullOrWhiteSpace(sErr))
            {
                resp.ErrorCode = 2999;
                resp.ErrorDesc = sErr;
            }
            else if (wsResp == null && string.IsNullOrWhiteSpace(sErr))
            {
                resp.ErrorCode = 9999;
                resp.ErrorDesc = "unable to create response";
            }

            //resp.ErrorDesc = string.Format("{0}, {1}, {2}", sBankCode, sAccountNumber, sRegisteredIP);

            LogMessage(string.Format("VBA BRI, {0}, {1}, end", sBankCode, sAccountNumber));

            return resp;
        }

        protected override void setDefault()
        {
            _wsRejectCodes_default = "";
            _wsErrorCodes_default = "";
            _wsOKCodess_default = "0001";
        }
    }
}