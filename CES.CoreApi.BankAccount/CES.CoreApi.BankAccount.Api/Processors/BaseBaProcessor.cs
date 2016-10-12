using CES.CoreApi.BankAccount.Api.Models;
using CES.CoreApi.BankAccount.Api.Repositories;
using CES.CoreApi.BankAccount.Api.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace CES.CoreApi.BankAccount.Service.Business.Logic.Processors
{
    public abstract class BaseBaProcessor
    {
        private BankAccountRespositoryCached _repository = new BankAccountRespositoryCached();

        private string _clientAppId = "";
     

        protected string[] _wsRejectCodes = null;
        protected string[] _wsErrorCodes = null;
        protected string[] _wsOKCodes = null;

        protected string _wsRejectCodes_default;
        protected string _wsErrorCodes_default;
        protected string _wsOKCodess_default;

        public int PaAgentID { get; set; }
        public int ProcessorID { get; set; }
        public Hashtable PaSetting { get; set; }
        public BankAccountValidationProcessor BaProcessor { get; set; }
        public abstract BankAccountInfo ValidateAccount(string sName, string sBankName, string sBankCode, string sAccountNumber);

        public CPaConn GetPaConn()
        {
            CPaConn paConn = new CPaConn(PaAgentID);

            if (TextUtil.GetStr(PaSetting["UseCoreApiWsSetting"]).Trim() == "1")
            {
                paConn.fHost = TextUtil.GetStr(PaSetting["Host"]);
                paConn.fLogonName = TextUtil.GetStr(PaSetting["LogonName"]);
                paConn.fLogonPassword = TextUtil.GetStr(PaSetting["LogonPassword"]);
                paConn.fAgentCode = TextUtil.GetStr(PaSetting["AgentCode"]);
            }
            else
            {
                paConn = _repository.GetPaWsSetting(PaAgentID);
            }

            string auto = TextUtil.GetStr(PaSetting["WsTimeOutMs"]);
            int nTimeOut;
            int.TryParse(auto, out nTimeOut);
            paConn.WsTimeOutMs = nTimeOut;

            return paConn;
        }
       
        public void LogMessage(string sMsg)
        {
            Logging.Log.Info(sMsg);
        }

     
        public bool AllowSoapDbLog
        {
            get
            {
                bool bAuto = true;
                string sAuto = TextUtil.GetStr(PaSetting["AllowSoapDbLog"]).ToLower();
                if (sAuto == "0" || sAuto == "false" || sAuto == "no") bAuto = false;

                return bAuto;
            }
        }

        public virtual void Initialize()
        {
            setDefault();
            getWsCodes();
        }

        protected virtual void setDefault()
        {
            _wsRejectCodes_default = "";
            _wsErrorCodes_default = "";
            _wsOKCodess_default = "";
        }

        protected virtual void getWsCodes()
        {
            string sAuto = TextUtil.GetStr(PaSetting["WsOKCodes"], _wsOKCodess_default);
            _wsOKCodes = createRegExps(sAuto);

            sAuto = TextUtil.GetStr(PaSetting["WsErrorCodes"], _wsErrorCodes_default);
            _wsErrorCodes = createRegExps(sAuto);

            sAuto = TextUtil.GetStr(PaSetting["WsRejectCodes"], _wsRejectCodes_default);
            _wsRejectCodes = createRegExps(sAuto);
        }

        protected string[] createRegExps(string source)
        {
            char[] seps = { '|' };
            StringBuilder sb = new StringBuilder();

            string[] codes = source.Split(seps, StringSplitOptions.RemoveEmptyEntries);

            foreach (string code in codes)
            {
                sb.Append('^');
                sb.Append(code);
                sb.Append('$');
                sb.Append('|');
            }

            if (sb.Length != 0) sb.Remove(sb.Length - 1, 1);

            if (sb.Length == 0) return null;

            return sb.ToString().Split(seps);
        }

        protected bool codeIsPresent(string[] expressionArray, string code)
        {
            if (expressionArray == null || expressionArray.Length == 0 || string.IsNullOrEmpty(code)) return false;

            foreach (string regexExp in expressionArray)
            {
                if (string.IsNullOrEmpty(regexExp)) continue;

                if (Regex.IsMatch(code, regexExp, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline))
                    return true;
            }

            return false;
        }

        protected bool isWsErrorCodePresent(string code)
        {
            return codeIsPresent(_wsErrorCodes, code);
        }

        protected bool isWsOKCodePresent(string code)
        {
            return codeIsPresent(_wsOKCodes, code);
        }
    }
}