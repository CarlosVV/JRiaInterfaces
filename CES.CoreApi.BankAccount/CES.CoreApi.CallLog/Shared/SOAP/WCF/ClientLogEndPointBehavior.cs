using System;
using System.Text;
using System.ServiceModel.Description;
using System.Xml;
using System.IO;

using CES.CoreApi.CallLog.Db;

namespace CES.CoreApi.CallLog.Shared.SOAP.WCF
{
    /// <summary>
    /// Behavior for WCF clients' logging
    /// </summary>
    public class ClientDBLogEndPointBehavior: IEndpointBehavior
    {
        //@@2013-19-25 lb SCR# 1877311 Created
        //@@2014-09-10 lb SCR# 2124211 (SCR used to allocated this change)
        //@@2015-02-06 lb SCR# 2235011 Enhanced logging 
        //@@2015-11-05 lb SCR# 2455111 Updated calls

        private string _resquestMessage = "";
        private string _responseMessage = "";

        private DateTime _resquestTS = DateTime.MinValue;
        private DateTime _responseTS = DateTime.MinValue;

        private bool _textOutputIsFormatted;
        private DBWsCallLogOps _dBWsCallLogOps = new DBWsCallLogOps();
        public class ClientContext
        {
            public int TransactionID { get; set; }
            public int ServiceID { get; set; }
            public string Context { get; set; }

            /// <summary>
            /// Indicates if we want an automatic saving in database
            /// </summary>
            public bool SuppressDBLog { get; set; }
        }

        private ClientContext _clientContext;

        /// <summary>
        /// Retrieves the client context
        /// </summary>
        public ClientContext ClientCAllContext
        {
            //@@2014-01-13 lb SCR# 1950711 Created
            get { return _clientContext; }
        }

        public ClientDBLogEndPointBehavior(ClientContext clientContext, bool textOutputIsFormatted)
        {
            if (clientContext == null)
                throw new ArgumentNullException("The client context object cannot be null.");

            _clientContext = clientContext;
            _textOutputIsFormatted = textOutputIsFormatted;
        }

        /// <summary>
        /// Creates a WCF client inspector. Text of reques and response are provided formatted
        /// </summary>
        /// <param name="clientContext"></param>
        public ClientDBLogEndPointBehavior(ClientContext clientContext):this(clientContext, true)
        {}

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ClientInspector(beforeSendRequest, afterReceivedReply));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            return;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }

        #endregion

        private void beforeSendRequest(string reqText, out Guid guid, string url)
        {
            guid = Guid.NewGuid();
            
            try
            {
                _resquestMessage = reqText;
                _resquestTS = DateTime.Now;

                if (_clientContext.SuppressDBLog) return;

                logInDatabase(reqText, guid, 1, url);
            }
            catch
            {
                //TODO: Pending to log errors
            }
        }

        private void afterReceivedReply(string respText, Guid guid, string url)
        {
            try
            {

                _responseMessage = respText;
                _responseTS = DateTime.Now;

                if (_clientContext.SuppressDBLog) return;

                logInDatabase(respText, guid, 2, url);
            }
            catch
            {
                //TODO: Pending to log errors
            }
        }

        private void logInDatabase(string text, Guid guid, int dumpType, string url)
        {
            _dBWsCallLogOps.WriteToLogAsXML((DataFlowDirection)dumpType, _clientContext.TransactionID, _clientContext.ServiceID, _clientContext.Context,
                text, guid, null, url);
        }

        /// <summary>
        /// Gets the request SOAP text
        /// </summary>
        public string RequestMessage {
            get {
                if (string.IsNullOrEmpty(_resquestMessage)) return "";

                if (!_textOutputIsFormatted)
                    return _resquestMessage;

                StringBuilder sb = new StringBuilder();

                using (XmlReader xmlReader = XmlReader.Create(new StringReader(_resquestMessage), new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true, IndentChars = "\t", OmitXmlDeclaration = true }))
                    {
                        while (!xmlReader.EOF)
                        {
                            xmlWriter.WriteNode(xmlReader, true);
                        }
                    }
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets the response's SOAP text
        /// </summary>
        public string ResponseMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_responseMessage)) return "";

                if (!_textOutputIsFormatted)
                    return _responseMessage;

                StringBuilder sb = new StringBuilder();

                using (XmlReader xmlReader = XmlReader.Create(new StringReader(_responseMessage), new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true, IndentChars = "\t", OmitXmlDeclaration = true }))
                    {
                        while (!xmlReader.EOF)
                        {
                            xmlWriter.WriteNode(xmlReader, true);
                        }
                    }
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets the time at which the request was sent
        /// </summary>
        public DateTime RequestTime {
            get { return _resquestTS; }
        }

        /// <summary>
        /// Gets the time at which the response was received
        /// </summary>
        public DateTime ResponseTime {
            get { return _responseTS; }
        }
    }
}
