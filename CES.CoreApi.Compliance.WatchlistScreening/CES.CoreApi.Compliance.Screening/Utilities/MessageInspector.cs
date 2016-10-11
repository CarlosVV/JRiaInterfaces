using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace CES.CoreApi.Compliance.Screening.Utilities
{
    public class MessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        public MessageInspector()
        {

        }

        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            Logging.Log.Info("AfterReceiveRequest");
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Logging.Log.Info("BeforeSendReply");
        }

        #endregion

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            ////TEST: Receive very long XML :o

            //MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
            //reply = buffer.CreateMessage();
            //var copy = buffer.CreateMessage();

            //XmlDocument oDoc = new XmlDocument();
            //using (XmlWriter writer = oDoc.CreateNavigator().AppendChild())
            //{
            //    copy.WriteMessage(writer);
            //    writer.Close();
            //}

            //Logging.Log.Info($"Response from Actimize:{Environment.NewLine} {FormatXml(oDoc.OuterXml)}");


        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
 
            MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);
            request = buffer.CreateMessage();
            var copy = buffer.CreateMessage(); 

            XmlDocument oDoc = new XmlDocument();          
            using (XmlWriter writer = oDoc.CreateNavigator().AppendChild())
            {
                copy.WriteMessage(writer);
                writer.Close();
            }

            //Remove Action Node
            XmlNode action = oDoc.GetElementsByTagName("Action")[0];
            if (action != null)
            {
                action.ParentNode.RemoveChild(action);
            }

            Logging.Log.Info($"Request to Actimize:{Environment.NewLine} {FormatXml(oDoc.OuterXml)}");

            return null;
        }

        string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }
        #endregion
    }
}
