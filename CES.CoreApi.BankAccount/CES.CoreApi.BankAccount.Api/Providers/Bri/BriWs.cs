using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace CES.CoreApi.BankAccount.Service.Business.Logic.WS_BRI
{
    public partial class Brifastservice : CES.CoreApi.CallLog.Shared.SOAP.WSExtension.Implemented.ITraceableDB
    {
        internal Brifastservice(int transactionID, bool bAllowDbLog)
        {
            IsTraceRequestEnabled = bAllowDbLog;
            IsTraceResponseEnabled = bAllowDbLog;
            TransactionID = 0;
            ServiceID = 111;
            Context = "Corresp web service, BRI";
        }

        public string RequestMessage { get; set; }

        public string ResponseMessage { get; set; }

        public bool IsTraceRequestEnabled { get; set; }

        public bool IsTraceResponseEnabled { get; set; }

        public int TransactionID { get; set; }

        public int ServiceID { get; set; }

        public string Context { get; set; }

        public DateTime RequestTime { get; set; }

        public DateTime ResponseTime { get; set; }
    }
}

namespace CES.CoreApi.BankAccount.Service.Business.Logic.Processors
{
    class BriWs
    {
        public string LastRequestMsg { get; set; }
        public string LastResponseMsg { get; set; }
        public BaseBaProcessor BaProcessor { get; set; }

        private string _webHost;
        private string _ria_AgentCode;
        private string _logonName;
        private string _logonPassword;
        private string _registeredIP;
        private int _wsTimeOut_ms = 0;

        private WS_BRI.Brifastservice _ws;
        private int _transID = 0;
        private string _wsContext = "";

        public BriWs(string sUrl, string AgentCode, string LogonName, string LogonPassword, string RegisteredIP, int nTimeOut = 0)
        {
            _webHost = sUrl;
            _ria_AgentCode = AgentCode;
            _logonName = LogonName;
            _logonPassword = LogonPassword;
            _registeredIP = RegisteredIP;
            _wsTimeOut_ms = nTimeOut;
        }

        private void initSoapMsg()
        {
            //_ws.IsTraceRequestEnabled = true;
            //_ws.IsTraceResponseEnabled = true;
            //_ws.TransactionID = _transID;
            //_ws.ServiceID = 111;
            //_ws.Context = _wsContext;

            LastRequestMsg = "";
            LastResponseMsg = "";
        }

        public void SetSoapMsg(int nTransactionID, string sContext)
        {
            _transID = nTransactionID;
            _wsContext = sContext;
        }

        private WS_BRI.requestTokenCTResult requestToken()
        {
            WS_BRI.requestTokenCT paramIn = new WS_BRI.requestTokenCT();
            paramIn.username = _logonName;
            paramIn.password = _logonPassword;
            paramIn.ipAddress = _registeredIP;
            paramIn.counterpart = _ria_AgentCode;

            WS_BRI.requestTokenCTResult paramOut = _ws.requestToken(paramIn);
            //LastRequestMsg += "<!--requestToken-->\n\r" + _ws.RequestMessage;
            //LastResponseMsg += "<!--requestToken-->\n\r" + _ws.ResponseMessage;

            return paramOut;

        }

        public bool InquiryAccount(string sBankCode, string sAccountNumber, out string sPAMessage, out string sErr)
        {
            bool bIsvalid = false;
            sPAMessage = ""; sErr = "";

            WS_BRI.inquiryAccount_CT_Result paramOut = InquiryAccount2(sBankCode, sAccountNumber, out sPAMessage, out sErr);

            sPAMessage = paramOut.statusCode + "|" + paramOut.message + "|" + paramOut.accountCurrency + "|" + paramOut.accountName;
            if (paramOut.statusCode == "0001") bIsvalid = true;

            return bIsvalid;
        }

        public WS_BRI.inquiryAccount_CT_Result InquiryAccount2(string sBankCode, string sAccountNumber, out string sPAMessage, out string sErr)
        {
            sPAMessage = ""; sErr = "";
            WS_BRI.inquiryAccount_CT_Result paramOut = null;

            try
            {
                _ws = new WS_BRI.Brifastservice(0, BaProcessor.AllowSoapDbLog);
                using (_ws)
                {
                    initSoapMsg();

                    if (_wsTimeOut_ms > 0) _ws.Timeout = _wsTimeOut_ms;
                    _ws.Url = _webHost;
                    WS_BRI.requestTokenCTResult resultToken = requestToken();
                    if (resultToken.statusCode != "0001")
                    {
                        sPAMessage = "requestToken err|" + resultToken.statusCode + "|" + resultToken.message;
                        return null;
                    }

                    WS_BRI.inquiryAccount_CT paramIn = new WS_BRI.inquiryAccount_CT();
                    paramIn.username = _logonName;
                    paramIn.password = _logonPassword;
                    paramIn.counterpart = _ria_AgentCode;
                    paramIn.ipAddress = _registeredIP;

                    paramIn.accountNumber = sAccountNumber;
                    paramIn.bankCode = sBankCode;
                    paramIn.token = resultToken.token;

                    paramOut = _ws.inquiryAccount(paramIn);
                    //LastRequestMsg += "\r\n\r\n<!--inquiryAccount-->\n\r" + _ws.RequestMessage;
                    //LastResponseMsg += "\r\n\r\n<!--inquiryAccount-->\n\r" + _ws.ResponseMessage;

                    sPAMessage = paramOut.statusCode + "|" + paramOut.message + "|" + paramOut.accountCurrency + "|" + paramOut.accountName;
                }
            }
            catch (Exception ex)
            {
                sErr = "Exception|" + ex.Message + "|loc:BriWs.inquiryAccount2";
            }

            return paramOut;
        }
        public WS_BRI.paymentAccount_CT_Result SendPO_BankDeposit(WS_BRI.paymentAccount_CT po, out string sPAMessage, out string sErr)
        {
            WS_BRI.paymentAccount_CT_Result r1 = null;
            sPAMessage = ""; sErr = "";

            try
            {
                using (_ws = new WS_BRI.Brifastservice())
                {
                    initSoapMsg();

                    if (_wsTimeOut_ms > 0) _ws.Timeout = _wsTimeOut_ms;
                    _ws.Url = _webHost;
                    WS_BRI.requestTokenCTResult resultToken = requestToken();
                    if (resultToken.statusCode != "0001")
                    {
                        sPAMessage = resultToken.statusCode + "|" + resultToken.message + "|requestToken";
                        return null;
                    }

                    po.username = _logonName;
                    po.password = _logonPassword;
                    po.counterpart = _ria_AgentCode;
                    po.ipAddress = _registeredIP;
                    po.token = resultToken.token;

                    r1 = _ws.paymentAccount(po);
                    //LastRequestMsg += "\r\n\r\n<!--paymentAccount-->\n\r" + _ws.RequestMessage;
                    //LastResponseMsg += "\r\n\r\n<!--paymentAccount-->\n\r" + _ws.ResponseMessage;

                    sPAMessage = r1.statusCode + "|" + r1.message + "|" + r1.ticketNumber;
                    //if (paramOut.statusCode == "0001") bIsvalid = true;
                }
            }
            catch (WebException ex)
            {
                string sResult;
                //string sErrDesc = "";//parse error code some where
                try
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        sResult = reader.ReadToEnd();
                    }
                }
                catch { };

                sErr = "WebException: " + ex.Message;
            }
            catch (Exception ex)
            {
                string sResp = ""; // _ws.ResponseMessage;
                if (!string.IsNullOrWhiteSpace(sResp)) r1 = parseResponse(sResp);
                sErr = "Exception|" + ex.Message + "|loc:BriWs.SendPO_BankDeposit";
            }

            return r1;
        }

        private WS_BRI.paymentAccount_CT_Result parseResponse(string sResult)
        {
            WS_BRI.paymentAccount_CT_Result briRsp = new WS_BRI.paymentAccount_CT_Result();
            string sName;

            using (XmlReader readerXML = XmlReader.Create(new StringReader(sResult)))
            {
                // Parse the file and display each of the nodes.
                while (readerXML.Read())
                {
                    XmlNodeType nType = readerXML.NodeType;
                    if (nType == XmlNodeType.Element)
                    {
                        sName = readerXML.Name.ToString();
                        switch (sName.Trim().ToLower())
                        {
                            case "message": briRsp.message = readerXML.ReadString(); break;
                            case "statuscode": briRsp.statusCode = readerXML.ReadString(); break;
                            case "token": briRsp.ticketNumber = readerXML.ReadString(); break;
                        }
                    }
                }
            }

            return briRsp;
        }

    }
}
