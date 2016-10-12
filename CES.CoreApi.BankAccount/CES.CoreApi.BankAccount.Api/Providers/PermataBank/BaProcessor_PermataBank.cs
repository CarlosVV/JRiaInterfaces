using CES.CoreApi.BankAccount.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CES.CoreApi.BankAccount.Api.Providers.PermataBank;
using CES.CoreApi.BankAccount.Api.Utilities;

namespace CES.CoreApi.BankAccount.Service.Business.Logic.Processors
{
    class BaProcessor_PermataBank : BaseBaProcessor
    {
        public override BankAccountInfo ValidateAccount(string sName, string sBankName, string sBankCode, string sAccountNumber)
        {
            LogMessage(string.Format("VBA PermataBank, {0}, {1}, start...", sBankCode, sAccountNumber));
            BankAccountInfo resp = new BankAccountInfo(ProcessorID, sName, sBankCode, sBankName, sAccountNumber);

            CPaConn paConn = GetPaConn();

            if (paConn.fHost == "" || paConn.fLogonName == "" || paConn.fLogonPassword == "" || paConn.fAgentCode == "")
            {
                resp.ErrorDesc = "Host/LogonName/LogonPwd/AgentCode setting missing;";
                resp.ErrorCode = 2001;
                return resp;
            }

            int nExportFormatID;
            int.TryParse(TextUtil.GetStr(PaSetting["ExportFormatID"]), out nExportFormatID);

            string url = paConn.fHost;
            string groupID = paConn.fAgentCode;
            string userID = paConn.fLogonName;
            string pwd = paConn.fLogonPassword;

            string pgpPbPubKeyID = TextUtil.GetStr(PaSetting["PgpPbPubKeyID"]);
            string pgpRiaPriKeyID = TextUtil.GetStr(PaSetting["PgpRiaPriKeyID"]);

            if (pgpPbPubKeyID == "" || pgpRiaPriKeyID == "")
            {
                resp.ErrorDesc = "pgpPbPubKeyID/pgpRiaPriKeyID setting missing;";
                resp.ErrorCode = 2001;
                return resp;
            }

            PermataBankWs ws = new PermataBankWs(url, groupID, userID, pwd, pgpPbPubKeyID, pgpRiaPriKeyID);
            ws.BaProcessor = this;

            string sRefID, sPAMessage, sErr;
            PermataBankWs.PermataBankResponse wsResp = ws.ValidateAccount2(sAccountNumber, sBankCode, sBankName, out sRefID, out sPAMessage, out sErr);
            LogMessage(string.Format("VBA PermataBank, {0}, {1}, {2} ", sRefID, sPAMessage, sErr));

            if (wsResp != null)
            {
                if (!string.IsNullOrWhiteSpace(wsResp.AcctName)) resp.fBankAcctHolderName = wsResp.AcctName;
                if (!string.IsNullOrWhiteSpace(wsResp.AcctNo)) resp.AccountNo = wsResp.AcctNo;

                if (!isWsErrorCodePresent(wsResp.RespCode)) resp.IsAccountValid = isWsOKCodePresent(wsResp.RespCode);
                resp.fStatusCode = wsResp.RespCode;
                if (resp.IsAccountValid.HasValue && resp.IsAccountValid.Value) resp.fStatusMsg = sPAMessage;
                else resp.CorrespErrMsg = wsResp.RespMsg;
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

            LogMessage(string.Format("VBA PermataBank, {0}, {1}, end", sBankCode, sAccountNumber));

            return resp;
        }

        protected override void setDefault()
        {
            _wsRejectCodes_default = "";
            _wsErrorCodes_default = "";
            _wsOKCodess_default = "00";
        }
    }
}
