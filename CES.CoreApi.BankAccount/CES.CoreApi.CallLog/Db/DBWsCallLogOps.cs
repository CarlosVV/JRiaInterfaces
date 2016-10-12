using System;
using System.Data.SqlTypes;
using System.Xml;
using System.IO;
using System.Threading;
using CES.CoreApi.CallLog.Shared;
using CES.CoreApi.CallLog.Enviroment;
using CES.Data.Sql;
using CES.CoreApi.CallLog.Tools;

namespace CES.CoreApi.CallLog.Db
{
    /// <summary>
    /// Handles all db logging routines for SOAP
    /// </summary>
    public  class DBWsCallLogOps
    {
        //@@2014-09-10 lb SCR# 2124211 Created (SCR used to allocated this change)
        //@@2015-02-06 lb SCR# 2235011 Enhanced logging 
        //@@2015-11-05 lb SCR# 2455111 Added overload to post binary content

        private SqlMapper _sqlMapper = DatabaseName.CreateSqlMapper();
        private static Guid getOwnerGuid()
        {
            try
            {
                IOwnerAProcessGuidProvider provider = SynchronizationContext.Current as IOwnerAProcessGuidProvider;

                if (provider == null) return Guid.Empty;

                return provider.CurrentGuid;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Writes a log entry as XML (or SOAP)
        /// </summary>
        /// <param name="type">1: Request, 2: Response</param>
        /// <param name="transactionID">The transaction id under which the operation is being done. 0, if it doesn't apply</param>
        /// <param name="serviceID">A lookup value as described in salestblServices table</param>
        /// <param name="context">A short description to indicate the context under which the log is saved</param>
        /// <param name="soapContent">The text or content intended to save</param>      
        private  void writeToLogAsXMLText(DataFlowDirection type, int transactionID, int serviceID, string context, string soapContent, Guid contextGuid, string url = "", string httpHeaders = "", int httpStatusCode = 0)
        {
            try
            {
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(soapContent)))
                {
                    using (var sql = _sqlMapper.CreateCommand(DatabaseName.Main, "sys_sp_LogSOAPCalls"))
                    {
                        sql.AddParam("@lAppID", AppInstance.Current.AppID);
                        sql.AddParam("@lAppInstanceID", AppInstance.Current.InstanceID);
                        sql.AddParam("@sAppName", AppInstance.Current.ApplicationTitle.Left(100));
                        sql.AddParam("@lQueue", AppInstance.Current.AppQueue);
                        sql.AddParam("@lTransactionID", transactionID);
                        sql.AddParam("@lServiceID", serviceID);
                        sql.AddParam("@sContext", (context ?? "").Left(512));
                        sql.AddParam("@lDumpType", (type == DataFlowDirection.Request ? 1 : (type == DataFlowDirection.Response ? 2 : 0)));
                        sql.AddParam("@sUrl", (url ?? "").Left(2048).DBNullIfEmpty());
                        sql.AddParam("@gContextGUI", contextGuid.DBNullIfEmpty());
                        sql.AddParam("@sHttpHeaders", (httpHeaders ?? "").Left(2048).DBNullIfEmpty());
                        sql.AddParam("@lHttpStatusCode", httpStatusCode.DBNullIfEmpty());
                        sql.AddParam("@xmlSOAPContent", new SqlXml(xmlReader));
                        sql.AddParam("@sTextContent", DBNull.Value);
                        sql.AddParam("@gOwnerContextGUI", getOwnerGuid().DBNullIfEmpty());
                        sql.Execute();
                    }
                 
                }
            }
            catch
            {
                WriteToLogAsText(type, transactionID, serviceID, context, soapContent, contextGuid, url, httpHeaders, httpStatusCode);
            }
        }

        /// <summary>
        /// Writes a log entry as XML (or SOAP)
        /// </summary>
        /// <param name="type">1: Request, 2: Response</param>
        /// <param name="transactionID">The transaction id under which the operation is being done. 0, if it doesn't apply</param>
        /// <param name="serviceID">A lookup value as described in salestblServices table</param>
        /// <param name="context">A short description to indicate the context under which the log is saved</param>
        /// <param name="soapContent">The text or content intended to save</param>
        /// <param name="soapBinContent">The text or content intended to save, if defined it has preemincence over the soapContent property</param>       
        public void WriteToLogAsXML(DataFlowDirection type, int transactionID, int serviceID, string context, string soapContent, Guid contextGuid, byte[] soapBinContent, string url = "", string httpHeaders = "", int httpStatusCode = 0)
        {
            try
            {
                if (soapBinContent == null || soapBinContent.Length == 0)
                {
                    writeToLogAsXMLText(type, transactionID, serviceID, context, soapContent, contextGuid, url, httpHeaders, httpStatusCode);
                    return;
                }

                using (MemoryStream ms = new MemoryStream(soapBinContent))
                {
                    using (XmlReader xmlReader = XmlReader.Create(ms))
                    {
                        using (var sql = _sqlMapper.CreateCommand(DatabaseName.Main, "sys_sp_LogSOAPCalls"))
                        {
                            sql.AddParam("@lAppID", AppInstance.Current.AppID);
                            sql.AddParam("@lAppInstanceID", AppInstance.Current.InstanceID);
                            sql.AddParam("@sAppName", AppInstance.Current.ApplicationTitle.Left(100));
                            sql.AddParam("@lQueue", AppInstance.Current.AppQueue);
                            sql.AddParam("@lTransactionID", transactionID);
                            sql.AddParam("@lServiceID", serviceID);
                            sql.AddParam("@sContext", (context ?? "").Left(512));
                            sql.AddParam("@lDumpType", (type == DataFlowDirection.Request ? 1 : (type == DataFlowDirection.Response ? 2 : 0)));
                            sql.AddParam("@sUrl", (url ?? "").Left(2048).DBNullIfEmpty());
                            sql.AddParam("@gContextGUI", contextGuid.DBNullIfEmpty());
                            sql.AddParam("@sHttpHeaders", (httpHeaders ?? "").Left(2048).DBNullIfEmpty());
                            sql.AddParam("@lHttpStatusCode", httpStatusCode.DBNullIfEmpty());
                            sql.AddParam("@xmlSOAPContent", new SqlXml(xmlReader));
                            sql.AddParam("@sTextContent", DBNull.Value);
                            sql.AddParam("@gOwnerContextGUI", getOwnerGuid().DBNullIfEmpty());
                            sql.Execute();
                        }
                    }
                }
            }
            catch
            {
                if (soapBinContent != null || soapBinContent.Length != 0 && string.IsNullOrEmpty(soapContent))
                {
                    using (MemoryStream ms = new MemoryStream(soapBinContent))
                    {
                        using (StreamReader textReader = new StreamReader(ms, true))
                        {
                            soapContent = textReader.ReadToEnd();
                        }
                    }
                }

                WriteToLogAsText(type, transactionID, serviceID, context, soapContent, contextGuid, url, httpHeaders, httpStatusCode);
            }
         
        }
        /// <summary>
        /// Writes a log entry as Text
        /// </summary>
        /// <param name="type">1: Request, 2: Response</param>
        /// <param name="transactionID">The transaction id under which the operation is being done. 0, if it doesn't apply</param>
        /// <param name="serviceID">A lookup value as described in salestblServices table</param>
        /// <param name="context">A short description to indicate the context under which the log is saved</param>
        /// <param name="soapContent">The text or content intended to save</param>       
        public  void WriteToLogAsText(DataFlowDirection type, int transactionID, int serviceID, string context, string soapContent, Guid contextGuid, string url = "", string httpHeaders = "", int httpStatusCode = 0)
        {
            try
            {
                using (var sql = _sqlMapper.CreateCommand(DatabaseName.Main, "sys_sp_LogSOAPCalls"))
                {
                    sql.AddParam("@lAppID", AppInstance.Current.AppID);
                    sql.AddParam("@lAppInstanceID", AppInstance.Current.InstanceID);
                    sql.AddParam("@sAppName", AppInstance.Current.ApplicationTitle.Left(100));
                    sql.AddParam("@lQueue", AppInstance.Current.AppQueue);
                    sql.AddParam("@lTransactionID", transactionID);
                    sql.AddParam("@lServiceID", serviceID);
                    sql.AddParam("@sContext", (context ?? "").Left(512));
                    sql.AddParam("@lDumpType", (type == DataFlowDirection.Request ? 1 : (type == DataFlowDirection.Response ? 2 : 0)));
                    sql.AddParam("@sUrl", (url ?? "").Left(2048).DBNullIfEmpty());
                    sql.AddParam("@gContextGUI", contextGuid.DBNullIfEmpty());
                    sql.AddParam("@sHttpHeaders", (httpHeaders ?? "").Left(2048).DBNullIfEmpty());
                    sql.AddParam("@lHttpStatusCode", httpStatusCode.DBNullIfEmpty());
                    sql.AddParam("@xmlSOAPContent", DBNull.Value);
                    sql.AddParam("@sTextContent", soapContent);
                    sql.AddParam("@gOwnerContextGUI", getOwnerGuid().DBNullIfEmpty());
                    sql.Execute();
                }

             
            }
            catch { }
        }
    }
}
