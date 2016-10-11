using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace PayoutService.Test
{
    [TestClass]
    class ConfirmPayout_Tests
    {
        string m_xmlNS = "http://schemas.xmlsoap.org/soap/envelope";
        int m_applicationID = 501;
        string m_applicationSessionId = "ABC123456";
        string m_referenceNumber = "32456";
        string m_referenceNumberType = "TransactionID";
        DateTime m_utcNow = DateTime.UtcNow;

        //Default calling agent values:
        int m_agentID = 113311;
        int m_agentLocID = 0;
        string m_userID = "80912";


        //[TestMethod]
        //public void ConfirmPayout_Test1()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00930970997";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        ConfirmPayoutRequest request = new ConfirmPayoutRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.IDExpirationDate = Convert.ToDateTime("2017-10-15");
        //        request.IDNumber = "7709225545874";

        //        //Call the service and get a response:
        //        ConfirmPayoutResponse response;
        //        using (var service = new PayoutServiceClient(
        //            "basicHttpBindingConfiguration_IPayoutService",
        //            "http://devts3.riadev.local:10044/PayoutService/PayoutService.svc"))
        //        {
        //            using (new OperationContextScope((IContextChannel)service.InnerChannel))
        //            {
        //                //Set Customer Headers.
        //                var appIDHeader = new MessageHeader<string>(m_applicationID.ToString(CultureInfo.InvariantCulture));
        //                var appSessionIDHeader = new MessageHeader<string>(m_applicationSessionId.ToString(CultureInfo.InvariantCulture));
        //                var RefNumHeader = new MessageHeader<string>(m_referenceNumber.ToString(CultureInfo.InvariantCulture));
        //                var RefNumTypeHeader = new MessageHeader<string>(m_referenceNumberType.ToString(CultureInfo.InvariantCulture));
        //                var timestampHeader = new MessageHeader<string>(m_utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appIDHeader.GetUntypedHeader("ApplicationId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appSessionIDHeader.GetUntypedHeader("ApplicationSessionId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumHeader.GetUntypedHeader("ReferenceNumber", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumTypeHeader.GetUntypedHeader("ReferenceNumberType", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(timestampHeader.GetUntypedHeader("Timestamp", m_xmlNS));
        //                //Make the call:
        //                response = service.ConfirmPayout(request);
        //            }
        //        }

        //        Assert.AreEqual("", response.ConfirmationNumber);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}

        //[TestMethod]
        //public void ConfirmPayout_AlreadyPaid()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00169039086";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        ConfirmPayoutRequest request = new ConfirmPayoutRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.IDExpirationDate = Convert.ToDateTime("2017-10-15");
        //        request.IDNumber = "7709225545874";

        //        //Call the service and get a response:
        //        ConfirmPayoutResponse response;
        //        using (var service = new PayoutServiceClient(
        //            "basicHttpBindingConfiguration_IPayoutService",
        //            "http://devts3.riadev.local:10044/PayoutService/PayoutService.svc"))
        //        {
        //            using (new OperationContextScope((IContextChannel)service.InnerChannel))
        //            {
        //                //Set Customer Headers.
        //                var appIDHeader = new MessageHeader<string>(m_applicationID.ToString(CultureInfo.InvariantCulture));
        //                var appSessionIDHeader = new MessageHeader<string>(m_applicationSessionId.ToString(CultureInfo.InvariantCulture));
        //                var RefNumHeader = new MessageHeader<string>(m_referenceNumber.ToString(CultureInfo.InvariantCulture));
        //                var RefNumTypeHeader = new MessageHeader<string>(m_referenceNumberType.ToString(CultureInfo.InvariantCulture));
        //                var timestampHeader = new MessageHeader<string>(m_utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appIDHeader.GetUntypedHeader("ApplicationId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appSessionIDHeader.GetUntypedHeader("ApplicationSessionId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumHeader.GetUntypedHeader("ReferenceNumber", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumTypeHeader.GetUntypedHeader("ReferenceNumberType", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(timestampHeader.GetUntypedHeader("Timestamp", m_xmlNS));
        //                //Make the call:
        //                response = service.ConfirmPayout(request);
        //            }
        //        }

        //        Assert.AreEqual("00169039086", response.ConfirmationNumber);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}

        //[TestMethod]
        //public void ConfirmPayout_EmptyPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        ConfirmPayoutRequest request = new ConfirmPayoutRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.IDExpirationDate = Convert.ToDateTime("2017-10-15");
        //        request.IDNumber = "7709225545874";

        //        //Call the service and get a response:
        //        ConfirmPayoutResponse response;
        //        using (var service = new PayoutServiceClient(
        //            "basicHttpBindingConfiguration_IPayoutService",
        //            "http://devts3.riadev.local:10044/PayoutService/PayoutService.svc"))
        //        {
        //            using (new OperationContextScope((IContextChannel)service.InnerChannel))
        //            {
        //                //Set Customer Headers.
        //                var appIDHeader = new MessageHeader<string>(m_applicationID.ToString(CultureInfo.InvariantCulture));
        //                var appSessionIDHeader = new MessageHeader<string>(m_applicationSessionId.ToString(CultureInfo.InvariantCulture));
        //                var RefNumHeader = new MessageHeader<string>(m_referenceNumber.ToString(CultureInfo.InvariantCulture));
        //                var RefNumTypeHeader = new MessageHeader<string>(m_referenceNumberType.ToString(CultureInfo.InvariantCulture));
        //                var timestampHeader = new MessageHeader<string>(m_utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appIDHeader.GetUntypedHeader("ApplicationId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appSessionIDHeader.GetUntypedHeader("ApplicationSessionId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumHeader.GetUntypedHeader("ReferenceNumber", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumTypeHeader.GetUntypedHeader("ReferenceNumberType", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(timestampHeader.GetUntypedHeader("Timestamp", m_xmlNS));
        //                //Make the call:
        //                response = service.ConfirmPayout(request);
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        //TODO: Shouldn't we get a better messages than this? (This is getting a null pointer right now!!)
        //        Assert.AreEqual("Server error encountered. All details have been logged.", e.Message);
        //    }
        //}

        //[TestMethod]
        //public void ConfirmPayout_NullPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = null;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        ConfirmPayoutRequest request = new ConfirmPayoutRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.IDExpirationDate = Convert.ToDateTime("2017-10-15");
        //        request.IDNumber = "7709225545874";

        //        //Call the service and get a response:
        //        ConfirmPayoutResponse response;
        //        using (var service = new PayoutServiceClient(
        //            "basicHttpBindingConfiguration_IPayoutService",
        //            "http://devts3.riadev.local:10044/PayoutService/PayoutService.svc"))
        //        {
        //            using (new OperationContextScope((IContextChannel)service.InnerChannel))
        //            {
        //                //Set Customer Headers.
        //                var appIDHeader = new MessageHeader<string>(m_applicationID.ToString(CultureInfo.InvariantCulture));
        //                var appSessionIDHeader = new MessageHeader<string>(m_applicationSessionId.ToString(CultureInfo.InvariantCulture));
        //                var RefNumHeader = new MessageHeader<string>(m_referenceNumber.ToString(CultureInfo.InvariantCulture));
        //                var RefNumTypeHeader = new MessageHeader<string>(m_referenceNumberType.ToString(CultureInfo.InvariantCulture));
        //                var timestampHeader = new MessageHeader<string>(m_utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appIDHeader.GetUntypedHeader("ApplicationId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appSessionIDHeader.GetUntypedHeader("ApplicationSessionId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumHeader.GetUntypedHeader("ReferenceNumber", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumTypeHeader.GetUntypedHeader("ReferenceNumberType", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(timestampHeader.GetUntypedHeader("Timestamp", m_xmlNS));
        //                //Make the call:
        //                response = service.ConfirmPayout(request);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //TODO: Shouldn't we get a better messages than this? (This is getting a null pointer right now!!)
        //        Assert.AreEqual("Server error encountered. All details have been logged.", e.Message);
        //    }
        //}

        //[TestMethod]
        //public void ConfirmPayout_NotGCPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "910045875";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        ConfirmPayoutRequest request = new ConfirmPayoutRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.IDExpirationDate = Convert.ToDateTime("2017-10-15");
        //        request.IDNumber = "7709225545874";

        //        //Call the service and get a response:
        //        ConfirmPayoutResponse response;
        //        using (var service = new PayoutServiceClient(
        //            "basicHttpBindingConfiguration_IPayoutService",
        //            "http://devts3.riadev.local:10044/PayoutService/PayoutService.svc"))
        //        {
        //            using (new OperationContextScope((IContextChannel)service.InnerChannel))
        //            {
        //                //Set Customer Headers.
        //                var appIDHeader = new MessageHeader<string>(m_applicationID.ToString(CultureInfo.InvariantCulture));
        //                var appSessionIDHeader = new MessageHeader<string>(m_applicationSessionId.ToString(CultureInfo.InvariantCulture));
        //                var RefNumHeader = new MessageHeader<string>(m_referenceNumber.ToString(CultureInfo.InvariantCulture));
        //                var RefNumTypeHeader = new MessageHeader<string>(m_referenceNumberType.ToString(CultureInfo.InvariantCulture));
        //                var timestampHeader = new MessageHeader<string>(m_utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appIDHeader.GetUntypedHeader("ApplicationId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appSessionIDHeader.GetUntypedHeader("ApplicationSessionId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumHeader.GetUntypedHeader("ReferenceNumber", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumTypeHeader.GetUntypedHeader("ReferenceNumberType", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(timestampHeader.GetUntypedHeader("Timestamp", m_xmlNS));
        //                //Make the call:
        //                response = service.ConfirmPayout(request);
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        //TODO: Shouldn't we get a better messages than this? (the real error is logged.)
        //        Assert.AreEqual("Server error encountered. All details have been logged.", e.Message);
        //    }
        //}

        //[TestMethod]
        //public void ConfirmPayout_Expired()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00930970997";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        ConfirmPayoutRequest request = new ConfirmPayoutRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.IDExpirationDate = Convert.ToDateTime("2017-10-15");
        //        request.IDNumber = "7709225545874";

        //        //Call the service and get a response:
        //        ConfirmPayoutResponse response;
        //        using (var service = new PayoutServiceClient(
        //            "basicHttpBindingConfiguration_IPayoutService",
        //            "http://devts3.riadev.local:10044/PayoutService/PayoutService.svc"))
        //        {
        //            using (new OperationContextScope((IContextChannel)service.InnerChannel))
        //            {
        //                //Set Customer Headers.
        //                var appIDHeader = new MessageHeader<string>(m_applicationID.ToString(CultureInfo.InvariantCulture));
        //                var appSessionIDHeader = new MessageHeader<string>(m_applicationSessionId.ToString(CultureInfo.InvariantCulture));
        //                var RefNumHeader = new MessageHeader<string>(m_referenceNumber.ToString(CultureInfo.InvariantCulture));
        //                var RefNumTypeHeader = new MessageHeader<string>(m_referenceNumberType.ToString(CultureInfo.InvariantCulture));
        //                var timestampHeader = new MessageHeader<string>(m_utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appIDHeader.GetUntypedHeader("ApplicationId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appSessionIDHeader.GetUntypedHeader("ApplicationSessionId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumHeader.GetUntypedHeader("ReferenceNumber", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumTypeHeader.GetUntypedHeader("ReferenceNumberType", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(timestampHeader.GetUntypedHeader("Timestamp", m_xmlNS));
        //                //Make the call:
        //                response = service.ConfirmPayout(request);
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual("Partner Network Service: GC:Call: Internal error1068:Transfer expired", e.Message);
        //    }
        //}

        //[TestMethod]
        //public void ConfirmPayout_BadPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "0088556644";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        ConfirmPayoutRequest request = new ConfirmPayoutRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.IDExpirationDate = Convert.ToDateTime("2017-10-15");
        //        request.IDNumber = "7709225545874";

        //        //Call the service and get a response:
        //        ConfirmPayoutResponse response;
        //        using (var service = new PayoutServiceClient(
        //            "basicHttpBindingConfiguration_IPayoutService",
        //            "http://devts3.riadev.local:10044/PayoutService/PayoutService.svc"))
        //        {
        //            using (new OperationContextScope((IContextChannel)service.InnerChannel))
        //            {
        //                //Set Customer Headers.
        //                var appIDHeader = new MessageHeader<string>(m_applicationID.ToString(CultureInfo.InvariantCulture));
        //                var appSessionIDHeader = new MessageHeader<string>(m_applicationSessionId.ToString(CultureInfo.InvariantCulture));
        //                var RefNumHeader = new MessageHeader<string>(m_referenceNumber.ToString(CultureInfo.InvariantCulture));
        //                var RefNumTypeHeader = new MessageHeader<string>(m_referenceNumberType.ToString(CultureInfo.InvariantCulture));
        //                var timestampHeader = new MessageHeader<string>(m_utcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appIDHeader.GetUntypedHeader("ApplicationId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(appSessionIDHeader.GetUntypedHeader("ApplicationSessionId", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumHeader.GetUntypedHeader("ReferenceNumber", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(RefNumTypeHeader.GetUntypedHeader("ReferenceNumberType", m_xmlNS));
        //                OperationContext.Current.OutgoingMessageHeaders.Add(timestampHeader.GetUntypedHeader("Timestamp", m_xmlNS));
        //                //Make the call:
        //                response = service.ConfirmPayout(request);
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        //TODO: Shouldn't we get a better messages than this? (the real error is logged.)
        //        Assert.AreEqual("Server error encountered. All details have been logged.", e.Message);
        //    }
        //}


    }
}
