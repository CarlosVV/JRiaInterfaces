using CES.CoreApi.BankAccount.Service.Business.Logic.Processors;
using CES.CoreApi.CallLog.Db;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;

namespace CES.CoreApi.BankAccount.Api.Providers.Bni
{
    class BniWs
    {
        public class BniWs_Response
        {
            public int ResponseCode { get; set; }
            public string BankCode { get; set; }
            public string AccountNumber { get; set; }
            public string AccountName { get; set; }
            public string ErrCode { get; set; }
            public string ErrDesc { get; set; }
            public string ErrException { get; set; }
            public string OkMsg { get; set; }

            public bool IsAccountValid
            {
                get
                {
                    bool bAuto = (ResponseCode == 0 && string.IsNullOrEmpty(ErrCode) && string.IsNullOrEmpty(ErrDesc));
                    return bAuto;
                }
            }

            public bool IsPoOk
            {
                get
                {
                    bool bAuto = OkMsg.Trim().ToLower() == "o.k.";
                    return bAuto;
                }
            }

            public BniWs_Response(int nResultType)
            {
                ResponseCode = nResultType;
                BankCode = "";
                AccountNumber = "";
                AccountName = "";
                ErrCode = "";
                ErrDesc = "";
                ErrException = "";

                OkMsg = "";
            }

            public string GetPaMessage()
            {
                string sAuto = "";
                if (!(string.IsNullOrWhiteSpace(ErrCode) && string.IsNullOrWhiteSpace(ErrDesc))) sAuto = ErrCode + "|" + ErrDesc;

                return sAuto;
            }
        };

        public string LastRequestMsg { get; set; }
        public string LastResponseMsg { get; set; }
        public string LastSignedMsg { get; set; }
        public BaseBaProcessor BaProcessor { get; set; }

        public byte[] PriKey { get; set; }

        private string _webHost;
        private string _ria_AgentCode;
        private string _pkCS12_FilePath;
        private string _pkCS12_pwd;
        private int _wsTimeOut_ms = 0;
        private DBWsCallLogOps _dBWsCallLogOps = new DBWsCallLogOps();

        public BniWs(string WebHost, string Ria_AgentCode, string PkCS12_FilePath, string PkCS12_Pwd, int nTimeOut = 0)
        {
            _webHost = WebHost;
            _ria_AgentCode = Ria_AgentCode;
            _wsTimeOut_ms = nTimeOut;
            _pkCS12_FilePath = PkCS12_FilePath;
            _pkCS12_pwd = PkCS12_Pwd;

            LastSignedMsg = "";
        }

        public string RsaSha1DSign(string sTextIn)
        {
            string sDigitalSign = "";

            try
            {
                X509Certificate2 crt = null;
                //if (string.IsNullOrWhiteSpace(_pkCS12_pwd)) crt = new X509Certificate2(PriKey);
                //else 
                crt = new X509Certificate2(PriKey, _pkCS12_pwd, X509KeyStorageFlags.MachineKeySet);

                if (crt.PrivateKey == null) throw new Exception(string.Format("unable to read private key, private key length:{0}", PriKey.Length));
                RSACryptoServiceProvider RSA1 = (RSACryptoServiceProvider)crt.PrivateKey;

                RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA1);
                RSAFormatter.SetHashAlgorithm("SHA1");
                SHA1Managed sha1 = new SHA1Managed();
                var hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sTextIn));
                byte[] SignedHashValue = RSAFormatter.CreateSignature(hash);
                sDigitalSign = Convert.ToBase64String(SignedHashValue);
            }
            catch (Exception ex)
            {
                throw new Exception("pwd len2:" + _pkCS12_pwd.Length.ToString() + "|key len:" + PriKey.Length.ToString() + "|" + ex.ToString());
            }

            return sDigitalSign;
        }

        private string buildRequestMsg_BankAcctValidate(string sBankCode, string sAccountNum, string sSign)
        {
            StringBuilder xmlNotes = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            using (XmlWriter xWriter = XmlWriter.Create(xmlNotes, xmlSettings))
            {
                xWriter.WriteStartDocument(true);
                {
                    xWriter.WriteStartElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                    {
                        xWriter.WriteAttributeString("xmlns", "q0", null, "http://service.bni.co.id/remm");
                        xWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                        xWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");

                        xWriter.WriteStartElement("soapenv", "Body", null);
                        {
                            xWriter.WriteStartElement("q0", "accountInfoInquiry", null);//q0: not allowed
                            {
                                //xWriter.WriteElementString("SenderCompanyID", "XmlUtils.RemoveInvalidXmlChars(SenderCompanyID)");

                                xWriter.WriteStartElement("header");
                                {
                                    xWriter.WriteElementString("clientId", _ria_AgentCode);
                                    xWriter.WriteElementString("signature", sSign);
                                }
                                xWriter.WriteEndElement();

                                xWriter.WriteElementString("bankCode", sBankCode);
                                xWriter.WriteElementString("accountNum", sAccountNum);

                            }
                            xWriter.WriteEndElement();
                        }
                        xWriter.WriteEndElement();
                    }
                    xWriter.WriteEndElement();
                }
                xWriter.WriteEndDocument();

                xWriter.Close();
            }

            return xmlNotes.ToString();
        }

        private BniWs_Response parseResponse(int nResult, string sResult, string sErr)
        {
            string sName;

            BniWs_Response bniResp = new BniWs_Response(nResult);
            bniResp.ErrException = sErr;
            if (string.IsNullOrWhiteSpace(sResult) || nResult == 2) return bniResp;

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
                            case "bankcode": bniResp.BankCode = readerXML.ReadString(); break;
                            case "accountnumber": bniResp.AccountNumber = readerXML.ReadString(); break;
                            case "accountname": bniResp.AccountName = readerXML.ReadString(); break;
                            case "errorcode": bniResp.ErrCode = readerXML.ReadString(); break;
                            case "errordescription": bniResp.ErrDesc = readerXML.ReadString(); break;
                            //case "okmessage": bniResp.OkMsg = readerXML.ReadString(); break;//problem reading
                            case "message": bniResp.OkMsg = readerXML.ReadString(); break;//problem reading
                        }
                    }
                }
            }

            return bniResp;
        }

        private void logSoapMsgToDb(CES.CoreApi.CallLog.Shared.DataFlowDirection type, Guid guid, byte[] soapBinContent)
        {
            if (!BaProcessor.AllowSoapDbLog) return;

            try
            {
                _dBWsCallLogOps.WriteToLogAsXML(type, 0, 111, "Corresp web service, BNI", "", guid, soapBinContent, _webHost);
            }
            catch { }
        }

        private int sendRequest(string sXml, out string result, out string sErr)
        {
            sErr = "";
            int nResult = 2;
            StreamReader readStream;
            result = "";

            HttpWebRequest req = null;
            HttpWebResponse rsp = null;

            LastRequestMsg = sXml;
            LastResponseMsg = "";

            byte[] dataByte = Encoding.UTF8.GetBytes(sXml);
            object obResp = null;

            Guid guid = Guid.NewGuid();
            logSoapMsgToDb(CoreApi.CallLog.Shared.DataFlowDirection.Request, guid, dataByte);

            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                //_webHost = "https://remitapi.bni.co.id:35001/remittance/incoming/ria";
                req = (HttpWebRequest)WebRequest.Create(_webHost);
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = "application/xml";
                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

                req.ContentLength = dataByte.Length;
                if (_wsTimeOut_ms > 0) req.Timeout = _wsTimeOut_ms;

                using (Stream reqOut = req.GetRequestStream())
                {
                    reqOut.Write(dataByte, 0, dataByte.Length);
                    reqOut.Flush();
                    reqOut.Close();
                }

                obResp = req.GetResponse();
                rsp = (HttpWebResponse)obResp;

                using (Stream responseStream = rsp.GetResponseStream())
                {
                    readStream = new StreamReader(responseStream, Encoding.UTF8);
                    result = readStream.ReadToEnd();
                }
                nResult = 0;
                rsp.Close();
            }
            catch (WebException ex)
            {
                nResult = 1;
                try
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
                catch { };

                sErr = "WebException2: " + ex.Message + "|" + _webHost;
            }
            catch (Exception ex)
            {
                nResult = 2;
                sErr = "Exception: " + ex.Message;
            }

            LastResponseMsg = result;
            if (result.Length > 0) logSoapMsgToDb(CallLog.Shared.DataFlowDirection.Response, guid, Encoding.UTF8.GetBytes(result));

            return nResult;
        }

        public bool validateBankAcct(string sBankCode, string sAccountNumber, out string sPAMessage, out int nErrCode, out string sErr)
        {// test account "113183203", BNI bank code:009I;
            bool bIsValid = false;
            sPAMessage = ""; nErrCode = 0; sErr = "";
            string sResult;
            string sAuto = _ria_AgentCode + sBankCode + sAccountNumber;
            LastSignedMsg = sAuto;

            BniWs_Response resp = null;

            try
            {
                string sSign = RsaSha1DSign(sAuto);

                string sXmlFinal = buildRequestMsg_BankAcctValidate(sBankCode, sAccountNumber, sSign);
                int nResult = sendRequest(sXmlFinal, out sResult, out sErr);

                resp = parseResponse(nResult, sResult, sErr);
                bIsValid = resp.IsAccountValid;
                sPAMessage = resp.GetPaMessage();
            }
            catch (Exception ex)
            {
                sErr = "Exception|" + ex.Message + "|loc:BniWs.validateBankAcct";
            }

            if (!bIsValid)
            {
                if (resp != null && resp.ResponseCode == 1) nErrCode = 3001;
                else nErrCode = 3999;
            }

            return bIsValid;
        }

        public BniWs_Response validateBankAcct(string sBankCode, string sAccountNumber)
        {// test account "113183203", BNI bank code:009I;
            string sResult, sErr;

            string sAuto = _ria_AgentCode + sBankCode + sAccountNumber;
            LastSignedMsg = sAuto;
            string sSign = RsaSha1DSign(sAuto);

            string sXmlFinal = buildRequestMsg_BankAcctValidate(sBankCode, sAccountNumber, sSign);
            int nResult = sendRequest(sXmlFinal, out sResult, out sErr);

            BniWs_Response resp = parseResponse(nResult, sResult, sErr);

            return resp;
        }

        private void LogMessage(string sMessage)
        {
            BaProcessor.LogMessage(sMessage);
        }

        public BniWs_Response validateBankAcct(string sBankCode, string sAccountNumber, out int nErrCode, out string sErr)
        {// test account "113183203", BNI bank code:009;
            LogMessage(string.Format("validateBankAcct BNI Bank code:{0}, Account number:{1}", sBankCode, sAccountNumber));
            nErrCode = 0; sErr = "";
            string sResult;
            string sAuto = _ria_AgentCode + sBankCode + sAccountNumber;
            LastSignedMsg = sAuto;

            BniWs_Response resp = null;

            try
            {
                LogMessage(string.Format("validateBankAcct BNI digital signature for {0}", sAuto));
                string sSign = RsaSha1DSign(sAuto);

                string sXmlFinal = buildRequestMsg_BankAcctValidate(sBankCode, sAccountNumber, sSign);
                LogMessage("validateBankAcct BNI sending request");
                int nResult = sendRequest(sXmlFinal, out sResult, out sErr);

                resp = parseResponse(nResult, sResult, sErr);
                if (resp != null)
                {
                    if (string.IsNullOrWhiteSpace(resp.AccountNumber)) resp.AccountNumber = sAccountNumber;
                    if (string.IsNullOrWhiteSpace(resp.BankCode)) resp.BankCode = sBankCode;
                }
            }
            catch (Exception ex)
            {
                sErr = "Exception|" + ex.Message + "|loc:BniWs.validateBankAcct";
            }

            if (resp != null)
            {
                if (!resp.IsAccountValid && resp.ResponseCode == 1) nErrCode = 3001;
            }
            else nErrCode = 3999;

            return resp;
        }

        public BniWs_Response SendPO(string sMsgToHash, string sXmlPO)
        {
            string sResult, sErr;
            LastSignedMsg = sMsgToHash;

            string sSign = RsaSha1DSign(sMsgToHash);
            string sFinalXml = buildRequestMsg_ProcessPO(sSign, sXmlPO);

            int nResult = sendRequest(sFinalXml, out sResult, out sErr);
            BniWs_Response resp = parseResponse(nResult, sResult, sErr);

            return resp;
        }

        private string buildRequestMsg_ProcessPO(string sSign, string sXmlPO)
        {
            StringBuilder xmlNotes = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            using (XmlWriter xWriter = XmlWriter.Create(xmlNotes, xmlSettings))
            {
                xWriter.WriteStartDocument(true);
                {
                    xWriter.WriteStartElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                    {
                        xWriter.WriteAttributeString("xmlns", "q0", null, "http://service.bni.co.id/remm");
                        xWriter.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                        xWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");

                        xWriter.WriteStartElement("soapenv", "Body", null);
                        {
                            xWriter.WriteStartElement("q0", "processPO", null);//q0: not allowed
                            {
                                //xWriter.WriteElementString("SenderCompanyID", "XmlUtils.RemoveInvalidXmlChars(SenderCompanyID)");

                                xWriter.WriteStartElement("header");
                                {
                                    xWriter.WriteElementString("clientId", _ria_AgentCode);
                                    xWriter.WriteElementString("signature", sSign);
                                }
                                xWriter.WriteEndElement();

                                xWriter.WriteRaw(sXmlPO);
                            }
                            xWriter.WriteEndElement();
                        }
                        xWriter.WriteEndElement();
                    }
                    xWriter.WriteEndElement();
                }
                xWriter.WriteEndDocument();

                xWriter.Close();
            }

            return xmlNotes.ToString();
        }

    }
}