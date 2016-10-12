using System;
using System.Xml;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace CES.CoreApi.CallLog.Shared.SOAP.WCF
{
    /// <summary>
    /// Inspector for WCF client's logging
    /// </summary>
    public class ClientInspector:IClientMessageInspector
    {
        //@@2013-19-25 lb SCR# 1877311 Created
        //@@2014-09-03 SCR# 2087811 - Used SCR to promote changes on this module
        //@@2014-09-10 lb SCR# 2124211 (SCR used to allocated this change)
        //@@2015-02-06 lb SCR# 2235011 Enhanced logging
        //@@2015-06-25 lb SCR# 2332311 Used SCR to deploy fix in this module

        private RequestDelegate _beforeSendRequest = null;
        private ReplyDelegate _afterReceiveReply = null;

        public ClientInspector(RequestDelegate beforeSendRequest, ReplyDelegate afterReceivedReply)
        {
            _beforeSendRequest = beforeSendRequest;
            _afterReceiveReply = afterReceivedReply;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (_afterReceiveReply == null) return;

            MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
            
            reply = buffer.CreateMessage();

            string req = "";
            Message msgCopy = buffer.CreateMessage();

            if (!msgCopy.IsEmpty)
            {
                req = msgCopy.ToString();
                using (XmlDictionaryReader reader = msgCopy.GetReaderAtBodyContents())
                {
                    req = req.Replace("... stream ...", reader.ReadOuterXml());
                }
            }
            msgCopy.Close();

            object[] correlationStateParts = correlationState as object[];

            Guid guid = Guid.Empty;
            string url = "";

            if (correlationStateParts != null && correlationStateParts.Length >= 2)
            {
                if (correlationStateParts[0] != null && correlationStateParts[0].GetType() == typeof(Guid))
                    guid = (Guid)correlationStateParts[0];

                url = (correlationStateParts[1] as string) ?? "";
            }

            _afterReceiveReply(req, guid, url);

            return;
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            if (_beforeSendRequest == null) return null;
            
            MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);

            request = buffer.CreateMessage();

            string req = "";
            Message msgCopy = buffer.CreateMessage();

            if (!msgCopy.IsEmpty)
            {
                req = msgCopy.ToString();
                using (XmlDictionaryReader reader = msgCopy.GetReaderAtBodyContents())
                {
                    req = req.Replace("... stream ...", reader.ReadOuterXml());
                }
            }
            msgCopy.Close();
            
            Guid retGuid = Guid.Empty;
            
            _beforeSendRequest(req, out retGuid, channel.RemoteAddress.Uri.AbsoluteUri);

            return new object[]{retGuid, channel.RemoteAddress.Uri.AbsoluteUri};
        }
    }
}
