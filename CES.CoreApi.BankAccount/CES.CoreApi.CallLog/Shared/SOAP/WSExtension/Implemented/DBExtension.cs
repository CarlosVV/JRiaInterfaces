using System;
using System.IO;
using System.Web.Services.Protocols;

using CES.CoreApi.CallLog.Db;

namespace CES.CoreApi.CallLog.Shared.SOAP.WSExtension.Implemented
{
    /// <summary>
    /// A SOAP Extension to log every call in main database
    /// </summary>
    internal class DBExtension:TraceExtensionBase<ITraceableDB>
    {
        //@@2012-06-18 lb SCR# 665511 Created
        //@@2014-09-10 lb SCR# 2124211 (SCR used to allocated this change)
        //@@2015-02-06 lb SCR# 2235011 Enhanced logging 
        private DBWsCallLogOps _dBWsCallLogOps = new DBWsCallLogOps();
        protected override ITraceableDB GetTraceable(System.Web.Services.Protocols.SoapMessage message)
        {
            SoapClientMessage clientMessage = message as SoapClientMessage;

            if (clientMessage != null)
            {
                return clientMessage.Client as ITraceableDB;
            }
            return null;
        }

        protected override void writeToLog(DataFlowDirection type, ITraceableDB traceable, string url, Guid contextGuid)
        {
            if (traceable == null) return;
            
            ITraceableDBV2 traceableV2 = traceable as ITraceableDBV2;

            if (traceableV2 != null && traceableV2.SuppressDBLog) return;

            this.TraceStream.InnerStream.Position = 0;

            byte[] bytes = ((MemoryStream)this.TraceStream.InnerStream).ToArray();

            _dBWsCallLogOps.WriteToLogAsXML(type, traceable.TransactionID, traceable.ServiceID, traceable.Context, "",
                contextGuid, bytes, url);

            this.TraceStream.InnerStream.Position = 0;
        }
    }
}
