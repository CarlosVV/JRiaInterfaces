using System;
using System.IO;
using System.Web.Services.Protocols;

namespace CES.CoreApi.CallLog.Shared.SOAP.WSExtension
{
    /// <summary>
    /// The SOAP Extension base from which all implementation should inherit
    /// </summary>
    /// <typeparam name="ITrac"></typeparam>
    public abstract class TraceExtensionBase<ITrac> : SoapExtension where ITrac:ITraceable
    {
        //@@2012-06-18 lb SCR# 665511 Created
        //@@2014-09-10 lb SCR# 2124211 (SCR used to allocated this change)
        //@@2015-02-06 lb SCR# 2235011 Enhanced logging 

        TraceExtensionStream _traceStream;
        private Guid _contextGuid;

        /// <summary>
        /// Replaces soap stream with our smart stream
        /// </summary>
        public override Stream ChainStream(Stream stream)
        {
            _traceStream = new TraceExtensionStream(stream);
            return _traceStream;
        }

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }

        public override object GetInitializer(Type WebServiceType)
        {
            return null;
        }

        public override void Initialize(object initializer)
        {
        }

        public override void ProcessMessage(SoapMessage message)
        {
            ITrac traceable = GetTraceable(message);

            //If proxy is not configured to be traced, return
            if (traceable == null) return;
            
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    //If tracing is enabled, switch to memory buffer
                    if (traceable.IsTraceRequestEnabled)
                    {
                        _traceStream.SwitchToNewStream();
                    }
                    break;
                case SoapMessageStage.AfterSerialize:
                    //If message was written to memory buffer, write its content to log and copy to the SOAP stream
                    if (_traceStream.IsNewStream)
                    {
                        _traceStream.Position = 0;
                        
                        writeToLog(DataFlowDirection.Request, traceable, message.Url, _contextGuid = Guid.NewGuid());

                        _traceStream.Position = 0;

                        _traceStream.CopyNewToOld();
                    }
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    //If tracing is enabled, copy SOAP stream to the new stream and write its content to log
                    if (traceable.IsTraceResponseEnabled)
                    {
                        _traceStream.SwitchToNewStream();

                        _traceStream.CopyOldToNew();

                        writeToLog(DataFlowDirection.Response, traceable, message.Url, _contextGuid);

                        _traceStream.Position = 0;
                    }
                    break;
            }
        }

        /// <summary>
        /// Tries to get ITraceable instance 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected abstract ITrac GetTraceable(SoapMessage message);

        protected abstract void writeToLog(DataFlowDirection type, ITrac traceable, string url, Guid contextGuid);

        protected TraceExtensionStream TraceStream
        {
            get { return _traceStream; }
        }
    }
}
