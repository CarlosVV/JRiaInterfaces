using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace CES.CoreApi.CallLog.Shared.SOAP.WCF
{
    public class ServiceInspector:IDispatchMessageInspector
    {
        //@@2015-06-25 lb SCR# 2332311 Used SCR to deploy fix in this module
        private RequestDelegate _afterReceiveRequest = null;
        private ReplyDelegate _beforeSendReply = null;

        public ServiceInspector(RequestDelegate afterReceiveRequest, ReplyDelegate beforeSendReply)
        {
            _afterReceiveRequest = afterReceiveRequest;
            _beforeSendReply = beforeSendReply;
        }

        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel,
            InstanceContext instanceContext)
        {
            if (_afterReceiveRequest == null) return null;

            MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);

            request = buffer.CreateMessage();

            string req = "";
            Message msgCopy = buffer.CreateMessage();

            if (!msgCopy.IsEmpty)
            {
                req = msgCopy.ToString();
                using (XmlDictionaryReader reader = msgCopy.GetReaderAtBodyContents())
                {
                    req = req.Replace("... stream ...",reader.ReadOuterXml());
                }
            }
            msgCopy.Close();
            
            Guid retGuid = Guid.Empty;

            string url = "";

            if (channel != null && channel.RemoteAddress != null && channel.RemoteAddress.Uri != null && !string.IsNullOrWhiteSpace
                (channel.RemoteAddress.Uri.AbsoluteUri))
                url = channel.RemoteAddress.Uri.AbsoluteUri;

            _afterReceiveRequest(req, out retGuid, url);

            return new object[] { retGuid, url };
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            if (_beforeSendReply == null) return;

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

            _beforeSendReply(req, guid, url);

            return;
        }

        #endregion
    }
}
