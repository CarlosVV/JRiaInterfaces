using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.ServiceModel;


namespace PayoutService.Test
{

    [TestClass]
    class PayoutTransaction_Tests
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
        int m_userID = 80912;


        //[TestMethod]
        //public void PayoutTransaction_Test1()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00714644257";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        int userID = m_userID;
        //        PayoutTransactionRequest request = new PayoutTransactionRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.Beneficiary.FirstName = "William";
        //        request.Beneficiary.LastName1 = "van Rooyen";
        //        request.Beneficiary.LastName2 = "";
        //        request.Beneficiary.MiddleName = "";
        //        request.Beneficiary.Address = "123 Main street";
        //        request.Beneficiary.City = "Santa Monica";
        //        request.Beneficiary.Country = "US";
        //        request.Beneficiary.IDIssuer = "RU";
        //        request.Beneficiary.IDNumber = "7709225545874";
        //        request.Beneficiary.IDSerialNumber = "";
        //        request.Beneficiary.IDType = "Passport";
        //        request.Beneficiary.PhoneNumber = "5551231123";

        //        //Call the service and get a response:
        //        PayoutTransactionResponse response;
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
        //                response = service.PayoutTransaction(request);
        //            }
        //        }

        //        Assert.AreEqual(0.49m, response.BeneficiaryFee.Amount);
        //        Assert.AreEqual(0.49m, response.BeneficiaryFee.CurrencyCode);
        //        Assert.AreEqual(0.49m, response.ConfirmationNumber);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}

        //[TestMethod]
        //public void PayoutTransaction_EmptyPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        PayoutTransactionRequest request = new PayoutTransactionRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.BeneficiaryFirstName = "William";
        //        request.BeneficiaryLastName1 = "van Rooyen";
        //        request.BeneficiaryLastName2 = "";
        //        request.BeneficiaryMiddleName = "";
        //        request.BeneficiaryAddress = "123 Main street";
        //        request.BeneficiaryCity = "Santa Monica";
        //        request.BeneficiaryCountry = "US";
        //        request.BeneficiaryIDIssuer = "RU";
        //        request.BeneficiaryIDNumber = "7709225545874";
        //        request.BeneficiaryIDSerialNumber = "";
        //        request.BeneficiaryIDType = "Passport";
        //        request.BeneficiaryPhoneNumber = "5551231123";

        //        //Call the service and get a response:
        //        PayoutTransactionResponse response;
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
        //                response = service.PayoutTransaction(request);
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
        //public void PayoutTransaction_NullPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = null;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        PayoutTransactionRequest request = new PayoutTransactionRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.BeneficiaryFirstName = "William";
        //        request.BeneficiaryLastName1 = "van Rooyen";
        //        request.BeneficiaryLastName2 = "";
        //        request.BeneficiaryMiddleName = "";
        //        request.BeneficiaryAddress = "123 Main street";
        //        request.BeneficiaryCity = "Santa Monica";
        //        request.BeneficiaryCountry = "US";
        //        request.BeneficiaryIDIssuer = "RU";
        //        request.BeneficiaryIDNumber = "7709225545874";
        //        request.BeneficiaryIDSerialNumber = "";
        //        request.BeneficiaryIDType = "Passport";
        //        request.BeneficiaryPhoneNumber = "5551231123";

        //        //Call the service and get a response:
        //        PayoutTransactionResponse response;
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
        //                response = service.PayoutTransaction(request);
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
        //public void PayoutTransaction_EmptyBenName()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00714644257";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        PayoutTransactionRequest request = new PayoutTransactionRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.BeneficiaryFirstName = "";
        //        request.BeneficiaryLastName1 = "";
        //        request.BeneficiaryLastName2 = "";
        //        request.BeneficiaryMiddleName = "";
        //        request.BeneficiaryAddress = "123 Main street";
        //        request.BeneficiaryCity = "Santa Monica";
        //        request.BeneficiaryCountry = "US";
        //        request.BeneficiaryIDIssuer = "RU";
        //        request.BeneficiaryIDNumber = "7709225545874";
        //        request.BeneficiaryIDSerialNumber = "";
        //        request.BeneficiaryIDType = "Passport";
        //        request.BeneficiaryPhoneNumber = "5551231123";

        //        //Call the service and get a response:
        //        PayoutTransactionResponse response;
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
        //                response = service.PayoutTransaction(request);
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
        //public void PayoutTransaction_BadPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "0055667788";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        PayoutTransactionRequest request = new PayoutTransactionRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.BeneficiaryFirstName = "William";
        //        request.BeneficiaryLastName1 = "van Rooyen";
        //        request.BeneficiaryLastName2 = "";
        //        request.BeneficiaryMiddleName = "";
        //        request.BeneficiaryAddress = "123 Main street";
        //        request.BeneficiaryCity = "Santa Monica";
        //        request.BeneficiaryCountry = "US";
        //        request.BeneficiaryIDIssuer = "RU";
        //        request.BeneficiaryIDNumber = "7709225545874";
        //        request.BeneficiaryIDSerialNumber = "";
        //        request.BeneficiaryIDType = "Passport";
        //        request.BeneficiaryPhoneNumber = "5551231123";

        //        //Call the service and get a response:
        //        PayoutTransactionResponse response;
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
        //                response = service.PayoutTransaction(request);
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
        //public void PayoutTransaction_NotGCPinlPin()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "910054185";
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string userID = m_userID;
        //        PayoutTransactionRequest request = new PayoutTransactionRequest();
        //        request.AgentID = agentID;
        //        request.AgentLocID = agentLocID;
        //        request.OrderPIN = pin;
        //        request.UserID = userID;
        //        request.BeneficiaryFirstName = "William";
        //        request.BeneficiaryLastName1 = "van Rooyen";
        //        request.BeneficiaryLastName2 = "";
        //        request.BeneficiaryMiddleName = "";
        //        request.BeneficiaryAddress = "123 Main street";
        //        request.BeneficiaryCity = "Santa Monica";
        //        request.BeneficiaryCountry = "US";
        //        request.BeneficiaryIDIssuer = "RU";
        //        request.BeneficiaryIDNumber = "7709225545874";
        //        request.BeneficiaryIDSerialNumber = "";
        //        request.BeneficiaryIDType = "Passport";
        //        request.BeneficiaryPhoneNumber = "5551231123";

        //        //Call the service and get a response:
        //        PayoutTransactionResponse response;
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
        //                response = service.PayoutTransaction(request);
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
