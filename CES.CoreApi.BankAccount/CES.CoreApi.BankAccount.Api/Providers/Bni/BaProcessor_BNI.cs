using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Providers.Bni;
using CES.CoreApi.BankAccount.Api.Repositories;
using CES.CoreApi.BankAccount.Api.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CES.CoreApi.BankAccount.Service.Business.Logic.Processors
{
    public class BaProcessor_BNI : BaseBaProcessor
    {//BNI: 25007111
        private BankAccountRespositoryCached _repository = new BankAccountRespositoryCached();
        public override BankAccountInfo ValidateAccount(string sName, string sBankName, string sBankCode, string sAccountNumber)
        {
            
            LogMessage(string.Format("VBA BNI, {0}, {1}, start...", sBankCode, sAccountNumber));
            BankAccountInfo resp = new BankAccountInfo(ProcessorID, sName, sBankCode, sBankName, sAccountNumber);

            CPaConn paConn = GetPaConn();
            if (paConn.fHost == "" || paConn.fAgentCode == "")
            {
                resp.ErrorDesc = "Host/AgentCode setting missing;";
                resp.ErrorCode = 2001;
                return resp;
            }

            int nExportFormatID;
            int.TryParse(TextUtil.GetStr(PaSetting["ExportFormatID"]), out nExportFormatID);

            string sPk12file, sPk12Pwd;
            getRsaInfo(nExportFormatID, out sPk12file, out sPk12Pwd);
            if (string.IsNullOrWhiteSpace(sPk12file)) throw new Exception("PK12 certificate file not provided");
            LogMessage(string.Format("VBA BNI PK12file, {0}", sPk12file));

            int nErrCode;
            string sErrorDesc;
            BniWs bniWs = new BniWs(paConn.fHost, paConn.fAgentCode, sPk12file, sPk12Pwd, paConn.WsTimeOutMs);
            bniWs.BaProcessor = this;
            LogMessage("VBA BNI BniWs created");
            bniWs.PriKey = _repository.LoadKeyFromDB(sPk12file);
            LogMessage("VBA BNI BniWs PriKey Loaded");
            BniWs.BniWs_Response bniResp = bniWs.validateBankAcct(sBankCode, sAccountNumber, out nErrCode, out sErrorDesc);
            LogMessage("VBA BNI BniResp created");

            if (bniResp != null)
            {
                LogMessage(string.Format("VBA BNI, {0}, {1}, {2} ", bniResp.GetPaMessage(), nErrCode, sErrorDesc));

                //resp.Provider = sName;
                resp.AccountNo = bniResp.AccountNumber;
                resp.fBankAcctHolderName = bniResp.AccountName;
                resp.BankCode = bniResp.BankCode;
                resp.fStatusCode = bniResp.ErrCode;

                resp.CorrespErrMsg = bniResp.ErrDesc;
                if (bniResp.ResponseCode == 0 && (!isWsErrorCodePresent(bniResp.ErrCode))) resp.IsAccountValid = bniResp.IsAccountValid;
                resp.fStatusMsg = bniResp.GetPaMessage();
            }

            resp.ErrorCode = nErrCode;
            resp.ErrorDesc = sErrorDesc;

            LogMessage(string.Format("VBA BNI, {0}, {1}, end", sBankCode, sAccountNumber));

            return resp;
        }

        private void getRsaInfo(int nExportFormatID, out string sPk12file, out string sPk12Pwd)
        {
            if (TextUtil.GetStr(PaSetting["UseCoreApiWsSetting"]).Trim() == "1")
            {
                sPk12file = TextUtil.GetStr(PaSetting["PKCS12_KeyLoc"]);
                sPk12Pwd = TextUtil.GetStr(PaSetting["PKCS12_Password"]);
            }
            else
            {
                Hashtable htPaExptSetting = _repository.GetPaExportSetting(nExportFormatID);
                sPk12file = TextUtil.GetStr(htPaExptSetting["PKCS12_KeyLoc"]);
                sPk12Pwd = TextUtil.GetStr(htPaExptSetting["PKCS12_Password"]);
            }
        }

        protected override void setDefault()
        {//998:falied to establish a backside connection
            _wsRejectCodes_default = "";
            _wsErrorCodes_default = "998";
            _wsOKCodess_default = "";
        }
    }
}