using CES.CoreApi.BankAccount.Api.Utilities;
using CES.CoreApi.BankAccount.Service.Business.Logic.Processors;
using CES.CoreApi.CallLog.Db;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace CES.CoreApi.BankAccount.Api.Providers.PermataBank
{
    class PermataBankWs
    {
        public class PermataBankResponse
        {
            public string RespMsg { get; set; }
            public string RespCode { get; set; }
            public string RespRtn { get; set; }
            public string AcctNo { get; set; }
            public string AcctName { get; set; }

            public PermataBankResponse()
            {
                RespMsg = "";
                RespCode = "";
                RespRtn = "";
                AcctNo = "";
                AcctName = "";
            }
        }

        public string LastRequestMsg { get; set; }
        public string LastResponseMsg { get; set; }
        public BaseBaProcessor BaProcessor { get; set; }

        private string _url;
        private string _groupID;
        private string _userID;
        private string _pwd;
        private int _wsTimeOut_ms = 0;

        private string _pgpPbPubKeyFile;
        private string _pgpRiaPriKeyFile;

        private DBWsCallLogOps _dBWsCallLogOps = new DBWsCallLogOps();
        public PermataBankWs(string url, string groupID, string userID, string pwd, string pgpPbPubKeyFile, string pgpRiaPriKeyFile, int nTimeOut = 0)
        {
            _url = url;
            _groupID = groupID;
            _userID = userID;
            _pwd = pwd;
            _wsTimeOut_ms = nTimeOut;

            _pgpPbPubKeyFile = pgpPbPubKeyFile;
            _pgpRiaPriKeyFile = pgpRiaPriKeyFile;
        }

        private string setXmlMsg(string sDecrypted, string sEncrypted)
        {
            string auto = string.Format("\r\n<!--arg1 un-encrypted:-->\r\n{0}\r\n\r\n<!--full message:-->\r\n{1}", sDecrypted, sEncrypted);
            return auto;
        }

        public PermataBankResponse SendTransaction(string sXml, out string sPaMsg, out string sErr)
        {
            PermataBankResponse rp = null;
            sPaMsg = ""; sErr = "";
            //check sum text: CORPID + CUSTREFNUMBER + DEBACCNUMBER + BENEFACCNUMBER + TRXDATE + TRXTIME + TRANSFERAMOUNT

            try
            {
                string sArg1 = PGP_Encrypt(sXml);
                string sFinalXml = buildRequestMsg("Request", sArg1);
                LastRequestMsg = setXmlMsg(sXml, sFinalXml);
                LastResponseMsg = "";

                PermataBankResponse rp0 = sendRequest(sFinalXml, out sErr);
                if (string.IsNullOrWhiteSpace(rp0.RespRtn)) return null;
                string sDecryptMsg = PGP_Decrypt(rp0.RespRtn);
                LastResponseMsg = setXmlMsg(sDecryptMsg, rp0.RespRtn);

                rp = parseResponse(sDecryptMsg);
                sPaMsg = rp.RespCode + "|" + rp.RespMsg + "|" + rp.AcctNo + "|" + rp.AcctName;
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }

            return rp;
        }

        public static string ComputeChecksum(string sInput)
        {
            byte[] bytInput = System.Text.Encoding.ASCII.GetBytes(sInput);
            byte[] hash;
            using (var md5 = MD5.Create())
            {
                hash = md5.ComputeHash(bytInput);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            string s1 = sb.ToString();

            return s1;
        }

        private string buildXml_AcctInqReq(string sAcctNo, string sCorpID, string sRefID)
        {
            StringBuilder xmlNotes = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            using (XmlWriter xWriter = XmlWriter.Create(xmlNotes, xmlSettings))
            {
                xWriter.WriteStartDocument(true);
                {
                    xWriter.WriteStartElement("AcctInqReq");
                    {
                        xWriter.WriteElementString("accountNumber", sAcctNo);
                        xWriter.WriteElementString("corpID", sCorpID);
                        xWriter.WriteElementString("refID", sRefID);
                    }
                    xWriter.WriteEndElement();
                }
                xWriter.WriteEndDocument();

                xWriter.Close();
            }

            return xmlNotes.ToString();
        }

        private string buildXml_BenefInqryReq(string sAcctNo, string sBankCode, string sBankName, string sRefID)
        {
            StringBuilder xmlNotes = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            using (XmlWriter xWriter = XmlWriter.Create(xmlNotes, xmlSettings))
            {
                xWriter.WriteStartDocument(true);
                {
                    xWriter.WriteStartElement("BenefInqryReq");
                    {
                        xWriter.WriteElementString("accountNumber", sAcctNo);
                        xWriter.WriteElementString("bankCode", sBankCode);
                        xWriter.WriteElementString("bankName", sBankName);
                        xWriter.WriteElementString("corpID", _groupID);
                        //xWriter.WriteElementString("corpID", _userID);
                        xWriter.WriteElementString("refID", sRefID);
                        xWriter.WriteElementString("userID", _userID);
                    }
                    xWriter.WriteEndElement();
                }
                xWriter.WriteEndDocument();

                xWriter.Close();
            }

            return xmlNotes.ToString();
        }

        private string getRefID()
        {
            string auto = DateTime.Now.ToString("yyyyMMddHHmmssff");
            if (auto.Length > 16) auto = auto.Substring(0, 16);

            return auto;
        }

        public bool ValidateAccount(string sAcctNo, out string sRefID, out string sPaMsg, out string sErr)
        {//refid:0878987654321
            bool bRtn = false;
            sPaMsg = ""; sErr = "";
            sRefID = getRefID();

            try
            {
                string sXml = buildXml_AcctInqReq(sAcctNo, _groupID, sRefID);//_userID or _groupID?
                string sArg1 = PGP_Encrypt(sXml);
                string sFinalXml = buildRequestMsg("AcctInqReq", sArg1);
                LastRequestMsg = setXmlMsg(sXml, sFinalXml);
                LastResponseMsg = "";

                PermataBankResponse rp0 = sendRequest(sFinalXml, out sErr); //web service is slow and timeout now.
                if (string.IsNullOrWhiteSpace(rp0.RespRtn)) return false;
                //string sReturn = @"hIwDvmsLRsSrKLABA/9AilXW/PMRQuVPq5GD9oaG8AJQiTzJIiu6NsZmNO9IjuEgom8vliLHXpxOqVRQl5mJ0BtWYbq+P5vL3+VxiA9yLmWgJ7ftSt95yQ3ARMyHndi4r/Jl97tBXZMZcVnC7gXuucvj59qaoJZxg06LAe/Y/F4Tv7LXPr6m1MKyto/6ZMmMSC9y6rtKIyKqRtCHIT+8dqbEfJTJuk7rWoKL1N659lzTavrVop/UavOFvWk/OhSGpXichPAfAxquYnYzmbfTqlOLYzqvUR1oAb45b0o7ABWw0Npm8p49osLpcFoUt2eJ2acwYafOkUMWRsx+qpX3/jymGGVSFQprHi10RsAaoNGclwHhGIlPlTK1lv0=";
                string sDecryptMsg = PGP_Decrypt(rp0.RespRtn);
                LastResponseMsg = setXmlMsg(sDecryptMsg, rp0.RespRtn);

                PermataBankResponse rp = parseResponse(sDecryptMsg);
                sPaMsg = rp.RespCode + "|" + rp.RespMsg + "|" + rp.AcctNo + "|" + rp.AcctName;

                if (rp.RespCode == "00") bRtn = true;
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }

            return bRtn;
        }

        public PermataBankResponse ValidateAccount2(string sAcctNo, string sBankCode, string sBankName, out string sRefID, out string sPaMsg, out string sErr)
        {//refid:0878987654321
            sPaMsg = ""; sErr = "";
            sRefID = getRefID();
            PermataBankResponse rp = null;
            string sXml, sArg1, sFinalXml;

            try
            {
                if (sBankCode == "013")
                {//Permata Bank
                    sXml = buildXml_AcctInqReq(sAcctNo, _groupID, sRefID);//_userID or _groupID?
                    sArg1 = PGP_Encrypt(sXml);
                    sFinalXml = buildRequestMsg("AcctInqReq", sArg1);
                }
                else
                {//Other banks
                    sXml = buildXml_BenefInqryReq(sAcctNo, sBankCode, sBankName, sRefID);//_userID or _groupID?
                    sArg1 = PGP_Encrypt(sXml);
                    sFinalXml = buildRequestMsg("BenefInqryReq", sArg1);
                }
                LastRequestMsg = setXmlMsg(sXml, sFinalXml);
                LastResponseMsg = "";

                PermataBankResponse rp0 = sendRequest(sFinalXml, out sErr); //web service is slow and timeout now.
                if (rp0 == null || string.IsNullOrWhiteSpace(rp0.RespRtn)) return rp0;
                string sDecryptMsg = PGP_Decrypt(rp0.RespRtn);
                LastResponseMsg = setXmlMsg(sDecryptMsg, rp0.RespRtn);

                rp = parseResponse(sDecryptMsg);
                sPaMsg = rp.RespCode + "|" + rp.RespMsg + "|" + rp.AcctNo + "|" + rp.AcctName;
            }
            catch (Exception ex)
            {
                sErr = ex.Message;
            }

            return rp;
        }

        private string PGP_Encrypt(string sInputText)
        {
            string sOutputText = "";
            var app = new AppUtil();
            try
            {
                sOutputText = SbPGP_Encrypt(_pgpPbPubKeyFile, sInputText);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sOutputText;
        }

        private string buildRequestMsg(string sArg0, string sArg1)
        {
            StringBuilder xmlNotes = new StringBuilder();
            XmlWriterSettings xmlSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            using (XmlWriter xWriter = XmlWriter.Create(xmlNotes, xmlSettings))
            {
                xWriter.WriteStartDocument(true);
                {
                    xWriter.WriteStartElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                    {
                        xWriter.WriteAttributeString("xmlns", "svc", null, "http://svc.proxy.h2h.pb.com/");

                        xWriter.WriteStartElement("soapenv", "Header", null);
                        xWriter.WriteEndElement();

                        xWriter.WriteStartElement("soapenv", "Body", null);
                        {
                            xWriter.WriteStartElement("svc", "execute", null);//q0: not allowed
                            {
                                xWriter.WriteElementString("arg0", sArg0);
                                xWriter.WriteElementString("arg1", sArg1);

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

        private void logSoapMsgToDb(CES.CoreApi.CallLog.Shared.DataFlowDirection type, Guid guid, byte[] soapBinContent)
        {
            if (!BaProcessor.AllowSoapDbLog) return;

            try
            {
                _dBWsCallLogOps.WriteToLogAsXML(type, 0, 111, "Corresp web service, Permata Bank", "", guid, soapBinContent, _url);
            }
            catch { }
        }

        private PermataBankResponse sendRequest(string sXml, out string sErr)
        {
            sErr = "";

            StreamReader readStream;
            string result;
            PermataBankResponse rp = null;

            HttpWebRequest req = null;
            HttpWebResponse rsp = null;

            byte[] dataByte = Encoding.UTF8.GetBytes(sXml);
            //byte[] dataByte = Encoding.ASCII.GetBytes(sXml);
            object obResp = null;

            Guid guid = Guid.NewGuid();
            logSoapMsgToDb(CoreApi.CallLog.Shared.DataFlowDirection.Request, guid, dataByte);

            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                req = (HttpWebRequest)WebRequest.Create(_url);
                req.Method = "POST";
                req.KeepAlive = true;
                req.ContentType = "text/xml;charset=UTF-8";
                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

                req.Headers.Add("Username", _userID);
                req.Headers.Add("Password", _pwd);
                req.Headers.Add("corpID", _groupID);

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

                rsp.Close();

                if (result.Length > 0) logSoapMsgToDb(CoreApi.CallLog.Shared.DataFlowDirection.Response, guid, Encoding.UTF8.GetBytes(result));

                result = result.Replace("&lt;", "<");
                rp = parseResponse(result);
            }
            catch (WebException ex)
            {
                try
                {
                    string sErrDesc = "";

                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        sErrDesc = reader.ReadToEnd();
                    }

                    if (sErrDesc.Length > 0) logSoapMsgToDb(CoreApi.CallLog.Shared.DataFlowDirection.Response, guid, Encoding.UTF8.GetBytes(sErrDesc));
                }
                catch { };

                sErr = "WebException: " + ex.Message;
            }
            catch (Exception ex)
            {
                sErr = "Exception: " + ex.Message;
            }

            return rp;
        }

        private PermataBankResponse parseResponse(string sResult)
        {
            PermataBankResponse rsp = new PermataBankResponse();
            string sName;

            using (XmlReader readerXML = XmlReader.Create(new StringReader(sResult)))
            {
                // Parse the file and display each of the nodes.
                while (readerXML.Read())
                {
                    XmlNodeType nType = readerXML.NodeType;
                    sName = readerXML.Name.ToString();

                    if (nType == XmlNodeType.Element)
                    {
                        switch (sName.Trim().ToLower())
                        {
                            case "return": rsp.RespRtn = readerXML.ReadString(); break;
                            case "responsemessage": rsp.RespMsg = readerXML.ReadString(); break;
                            case "responsecode": rsp.RespCode = readerXML.ReadString(); break;
                            case "acctno": rsp.AcctNo = readerXML.ReadString(); break;
                            case "acctname": rsp.AcctName = readerXML.ReadString(); break;
                        }
                    }
                }
            }

            return rsp;
        }

        private string PGP_Decrypt(string sInputText)
        {
            string sOutputText = "";

            try
            {
                sOutputText = SbPGP_Decrypt(_pgpRiaPriKeyFile, sInputText);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sOutputText;
        }

        public static string SbPGP_Decrypt(string sPGPPriKeyID, string sInputText)
        {
            CES.CoreApi.Crypto.PGP pgpObj = new CoreApi.Crypto.PGP(CES.CoreApi.Crypto.PGP.PGPLibraryEnum.SecureBlackBox);
            pgpObj.CryptoKeyID = sPGPPriKeyID;

            byte[] data_in = Convert.FromBase64String(sInputText);
            byte[] data_out;

            bool bRtn = pgpObj.PGPKeyDecryptAndVerifyByteArray(data_in, out data_out, "");//sPGPPriKeyPwd
            string sOutputText = System.Text.Encoding.UTF8.GetString(data_out.ToArray());

            return sOutputText;
        }

        public static string SbPGP_Encrypt(string sPGPKeyID, string sInputText)
        {
            CES.CoreApi.Crypto.PGP pgpObj = new CoreApi.Crypto.PGP(CES.CoreApi.Crypto.PGP.PGPLibraryEnum.SecureBlackBox);
            pgpObj.CryptoKeyID = sPGPKeyID;
            pgpObj.Armor = false;
            pgpObj.Compress = true;
            pgpObj.SymmetricCipherAlgorithm = CoreApi.Crypto.PGP.SymmetricCipherAlgorithmsEnum.CipherAlgorithm_CAST5;

            string sOutputText = "";

            byte[] data_in = System.Text.Encoding.UTF8.GetBytes(sInputText);
            byte[] data_out;

            bool bRtn = pgpObj.EncryptByteArray(data_in, out data_out, "");
            sOutputText = Convert.ToBase64String(data_out.ToArray());

            return sOutputText;
        }
    }
}