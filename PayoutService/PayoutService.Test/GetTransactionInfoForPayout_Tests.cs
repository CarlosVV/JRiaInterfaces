using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.ServiceModel;


namespace PayoutService.Test
{

    [TestClass]
    class GetTransactionInfoForPayout_Tests
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
        string m_agentCountry = "RU";
        string m_agentState = "";
        int m_appID = 501;
        int m_appObjectID = 0;
        string versionNum = "0.01";
        int m_userID = 80912;


        //[TestMethod]
        //public void GetTransactionInfo_NotGCPIN()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "1155886677";
        //        int orderID = 0;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string agentcountry = m_agentCountry;
        //        string agentstate = m_agentState;
        //        int userID = m_userID;
        //        string countryTo = "RU";
        //        string stateTo = "M";
        //        GetTransactionInfoRequest request = new GetTransactionInfoRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.RequesterInfo.AgentCountry = agentcountry;
        //        request.RequesterInfo.AgentState = agentstate;
        //        request.RequesterInfo.AppID = m_appID;
        //        request.RequesterInfo.AppObjectID = m_appObjectID;
        //        request.RequesterInfo.Locale = "PST";
        //        request.RequesterInfo.LocalTime = DateTime.Now;
        //        request.RequesterInfo.TerminalID = "DMG";
        //        request.RequesterInfo.TerminalUserID = userID.ToString();
        //        request.RequesterInfo.Timezone = "PST";
        //        request.RequesterInfo.TimezoneID = 8;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.RequesterInfo.UserID = 80912;
        //        request.RequesterInfo.UtcTime = DateTime.UtcNow;
        //        request.RequesterInfo.Version = versionNum;
        //        request.OrderID = orderID;
        //        request.OrderPIN = pin;

        //        //Call the service and get a response:
        //        GetTransactionInfoResponse response;
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
        //                response = service.GetTransactionInfo(request);
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
        //public void GetTransactionInfo_PaidOut_1()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00714644257";
        //        int orderID = 0;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string agentcountry = m_agentCountry;
        //        string agentstate = m_agentState;
        //        int userID = m_userID;
        //        string countryTo = "RU";
        //        string stateTo = "M";
        //        GetTransactionInfoRequest request = new GetTransactionInfoRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.RequesterInfo.AgentCountry = agentcountry;
        //        request.RequesterInfo.AgentState = agentstate;
        //        request.RequesterInfo.AppID = m_appID;
        //        request.RequesterInfo.AppObjectID = m_appObjectID;
        //        request.RequesterInfo.Locale = "PST";
        //        request.RequesterInfo.LocalTime = DateTime.Now;
        //        request.RequesterInfo.TerminalID = "DMG";
        //        request.RequesterInfo.TerminalUserID = userID.ToString();
        //        request.RequesterInfo.Timezone = "PST";
        //        request.RequesterInfo.TimezoneID = 8;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.RequesterInfo.UserID = 80912;
        //        request.RequesterInfo.UtcTime = DateTime.UtcNow;
        //        request.RequesterInfo.Version = versionNum;
        //        request.OrderID = orderID;
        //        request.OrderPIN = pin;

        //        //Call the service and get a response:
        //        GetTransactionInfoResponse response;
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
        //                response = service.GetTransactionInfo(request);
        //            }
        //        }

        //        Assert.AreEqual("Paid Out", response.TransferStatus);
        //        //TODO: Some values are coming back null if not set. Make this commission a 0.00m instead?
        //        Assert.AreEqual(0.00m, response.Comission.Amount);
        //        Assert.AreEqual("USD", response.Comission.CurrencyCode);
        //        Assert.AreEqual("RU", response.CountryFrom);
        //        Assert.AreEqual("", response.CountryTo);
        //        Assert.AreEqual(null, response.ExchangeRate);
        //        Assert.AreEqual(null, response.FromAgentID);
        //        Assert.AreEqual(false, response.OnLegalHold);
        //        Assert.AreEqual("00714644257", response.OrderID);
        //        Assert.AreEqual(null, response.PayDataMessage);
        //        Assert.AreEqual(Convert.ToDateTime("5/26/2014 3:49:35 AM"), response.PayDataNotAfterDate);
        //        Assert.AreEqual(true, response.PayDataNotAfterDateSpecified);
        //        Assert.AreEqual(Convert.ToDateTime("1/1/0001 12:00:00 AM"), response.PayDataNotBeforeDate);
        //        Assert.AreEqual(false, response.PayDataNotBeforeDateSpecified);
        //        Assert.AreEqual(510.00m, response.PayoutAmount.Amount);
        //        Assert.AreEqual("EUR", response.PayoutAmount.CurrencyCode);
        //        Assert.AreEqual("й", response.ReceiverAddress);
        //        Assert.AreEqual("н", response.ReceiverCity);
        //        Assert.AreEqual(2.55m, response.ReceiverComission.Amount);
        //        Assert.AreEqual("EUR", response.ReceiverComission.CurrencyCode);
        //        Assert.AreEqual(null, response.ReceiverCountry);
        //        Assert.AreEqual(null, response.ReceiverEmail);
        //        Assert.AreEqual("INES", response.ReceiverFirstName);
        //        Assert.AreEqual("MARIA INES", response.ReceiverFullName);
        //        Assert.AreEqual("", response.ReceiverIDIssuer);
        //        Assert.AreEqual("", response.ReceiverIDNumber);
        //        Assert.AreEqual("", response.ReceiverIDSerialNumber);
        //        Assert.AreEqual(-1, response.ReceiverIDTypeID);
        //        Assert.AreEqual("", response.ReceiverIDTypeText);
        //        Assert.AreEqual(null, response.ReceiverIsResident);
        //        Assert.AreEqual("ИВАНОВ", response.ReceiverLastName1);
        //        Assert.AreEqual("", response.ReceiverLastName2);
        //        Assert.AreEqual("ИВАНОВИЧ", response.ReceiverMiddleName);
        //        Assert.AreEqual("+79000000000", response.ReceiverPhone);
        //        Assert.AreEqual("N/A", response.ReceiverState);
        //        Assert.AreEqual(null, response.SendAmount);
        //        Assert.AreEqual("й", response.SenderAddress);
        //        Assert.AreEqual("н", response.SenderCity);
        //        Assert.AreEqual(0.15m, response.SenderComission.Amount);
        //        Assert.AreEqual("USD", response.SenderComission.CurrencyCode);
        //        Assert.AreEqual("RU", response.SenderCountry);
        //        Assert.AreEqual(null, response.SenderEmail);
        //        Assert.AreEqual("ВАЛЕНТИН", response.SenderFirstName);
        //        Assert.AreEqual("ПЕТРОВ ВАЛЕНТИН ВАСИЛЬЕВИЧ", response.SenderFullName);
        //        Assert.AreEqual(null, response.SenderIDIssuer);
        //        Assert.AreEqual("900000", response.SenderIDNumber);
        //        Assert.AreEqual("7888", response.SenderIDSerialNumber);
        //        Assert.AreEqual(0, response.SenderIDTypeID);
        //        Assert.AreEqual("Passport", response.SenderIDTypeText);
        //        Assert.AreEqual("1", response.SenderIsResident);
        //        Assert.AreEqual("ПЕТРОВ", response.SenderLastName1);
        //        Assert.AreEqual("", response.SenderLastName2);
        //        Assert.AreEqual("ВАСИЛЬЕВИЧ", response.SenderMiddleName);
        //        Assert.AreEqual("+79000000000", response.SenderPhone);
        //        Assert.AreEqual("N/A", response.SenderState);
        //        Assert.AreEqual(Convert.ToDateTime("3/27/2014 3:49:35 AM"), response.TransferDate);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}


        //[TestMethod]
        //public void GetTransactionInfo_ReadyPay()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00384805484";
        //        int orderID = 0;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string agentcountry = m_agentCountry;
        //        string agentstate = m_agentState;
        //        int userID = m_userID;
        //        string countryTo = "RU";
        //        string stateTo = "M";
        //        GetTransactionInfoRequest request = new GetTransactionInfoRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.RequesterInfo.AgentCountry = agentcountry;
        //        request.RequesterInfo.AgentState = agentstate;
        //        request.RequesterInfo.AppID = m_appID;
        //        request.RequesterInfo.AppObjectID = m_appObjectID;
        //        request.RequesterInfo.Locale = "PST";
        //        request.RequesterInfo.LocalTime = DateTime.Now;
        //        request.RequesterInfo.TerminalID = "DMG";
        //        request.RequesterInfo.TerminalUserID = userID.ToString();
        //        request.RequesterInfo.Timezone = "PST";
        //        request.RequesterInfo.TimezoneID = 8;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.RequesterInfo.UserID = 80912;
        //        request.RequesterInfo.UtcTime = DateTime.UtcNow;
        //        request.RequesterInfo.Version = versionNum;
        //        request.OrderID = orderID;
        //        request.OrderPIN = pin;

        //        //Call the service and get a response:
        //        GetTransactionInfoResponse order;
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
        //                order = service.GetTransactionInfo(request);
        //            }
        //        }

        //        Assert.AreEqual(0.31m, order.Comission.Amount);
        //        Assert.AreEqual("USD", order.Comission.CurrencyCode);
        //        Assert.AreEqual("RU", order.CountryFrom);
        //        Assert.AreEqual("ES", order.CountryTo);
        //        Assert.AreEqual(null, order.ExchangeRate);
        //        Assert.AreEqual("AG00SYS", order.FromAgentID);
        //        Assert.AreEqual(false, order.OnLegalHold);
        //        Assert.AreEqual("384805484", order.OrderID);
        //        Assert.AreEqual(null, order.PayDataMessage);
        //        Assert.AreEqual(Convert.ToDateTime("9/29/2014 8:35:46 PM"), order.PayDataNotAfterDate);
        //        Assert.AreEqual(true, order.PayDataNotAfterDateSpecified);
        //        Assert.AreEqual(Convert.ToDateTime("1/1/0001 12:00:00 AM"), order.PayDataNotBeforeDate);
        //        Assert.AreEqual(false, order.PayDataNotBeforeDateSpecified);
        //        Assert.AreEqual(31.00m, order.PayoutAmount.Amount);
        //        Assert.AreEqual("USD", order.PayoutAmount.CurrencyCode);
        //        Assert.AreEqual("Н", order.ReceiverAddress);
        //        Assert.AreEqual("Н", order.ReceiverCity);
        //        Assert.AreEqual(0.22m, order.ReceiverComission.Amount);
        //        Assert.AreEqual("USD", order.ReceiverComission.CurrencyCode);
        //        Assert.AreEqual(null, order.ReceiverCountry);
        //        Assert.AreEqual(null, order.ReceiverEmail);
        //        Assert.AreEqual("ПОРШЛ", order.ReceiverFirstName);
        //        Assert.AreEqual("АРППОРЛ ПОРШЛ ПЕГНПГО", order.ReceiverFullName);
        //        Assert.AreEqual("", order.ReceiverIDIssuer);
        //        Assert.AreEqual("", order.ReceiverIDNumber);
        //        Assert.AreEqual("", order.ReceiverIDSerialNumber);
        //        Assert.AreEqual(-1, order.ReceiverIDTypeID);
        //        Assert.AreEqual("", order.ReceiverIDTypeText);
        //        Assert.AreEqual(null, order.ReceiverIsResident);
        //        Assert.AreEqual("АРППОРЛ", order.ReceiverLastName1);
        //        Assert.AreEqual("", order.ReceiverLastName2);
        //        Assert.AreEqual("ПЕГНПГО", order.ReceiverMiddleName);
        //        Assert.AreEqual("+38000000000", order.ReceiverPhone);
        //        Assert.AreEqual("N/A", order.ReceiverState);
        //        Assert.AreEqual(null, order.SendAmount);
        //        Assert.AreEqual("Н", order.SenderAddress);
        //        Assert.AreEqual("Н", order.SenderCity);
        //        Assert.AreEqual(0.09m, order.SenderComission.Amount);
        //        Assert.AreEqual("USD", order.SenderComission.CurrencyCode);
        //        Assert.AreEqual("RU", order.SenderCountry);
        //        Assert.AreEqual(null, order.SenderEmail);
        //        Assert.AreEqual("АНТОН", order.SenderFirstName);
        //        Assert.AreEqual("УКОЛОВ АНТОН ИВАНОВИЧ", order.SenderFullName);
        //        Assert.AreEqual("КЕНКАН", order.SenderIDIssuer);
        //        Assert.AreEqual("808080", order.SenderIDNumber);
        //        Assert.AreEqual("9076", order.SenderIDSerialNumber);
        //        Assert.AreEqual(0, order.SenderIDTypeID);
        //        Assert.AreEqual("Passport", order.SenderIDTypeText);
        //        Assert.AreEqual("1", order.SenderIsResident);
        //        Assert.AreEqual("УКОЛОВ", order.SenderLastName1);
        //        Assert.AreEqual("", order.SenderLastName2);
        //        Assert.AreEqual("ИВАНОВИЧ", order.SenderMiddleName);
        //        Assert.AreEqual("+79000000000", order.SenderPhone);
        //        Assert.AreEqual("N/A", order.SenderState);
        //        Assert.AreEqual(Convert.ToDateTime("7/31/2014 8:35:46 PM"), order.TransferDate);
        //        Assert.AreEqual("Ready To Pay", order.TransferStatus);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}


        //[TestMethod]
        //public void GetTransactionInfo_Paid()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "00714644257";
        //        int orderID = 0;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string agentcountry = m_agentCountry;
        //        string agentstate = m_agentState;
        //        int userID = m_userID;
        //        string countryTo = "RU";
        //        string stateTo = "M";
        //        GetTransactionInfoRequest request = new GetTransactionInfoRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.RequesterInfo.AgentCountry = agentcountry;
        //        request.RequesterInfo.AgentState = agentstate;
        //        request.RequesterInfo.AppID = m_appID;
        //        request.RequesterInfo.AppObjectID = m_appObjectID;
        //        request.RequesterInfo.Locale = "PST";
        //        request.RequesterInfo.LocalTime = DateTime.Now;
        //        request.RequesterInfo.TerminalID = "DMG";
        //        request.RequesterInfo.TerminalUserID = userID.ToString();
        //        request.RequesterInfo.Timezone = "PST";
        //        request.RequesterInfo.TimezoneID = 8;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.RequesterInfo.UserID = 80912;
        //        request.RequesterInfo.UtcTime = DateTime.UtcNow;
        //        request.RequesterInfo.Version = versionNum;
        //        request.OrderID = orderID;
        //        request.OrderPIN = pin;

        //        //Call the service and get a response:
        //        GetTransactionInfoResponse order;
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
        //                order = service.GetTransactionInfo(request);
        //            }
        //        }

        //        Assert.AreEqual(1.21m, order.Comission.Amount);
        //        Assert.AreEqual("USD", order.Comission.CurrencyCode);
        //        Assert.AreEqual("RU", order.CountryFrom);
        //        Assert.AreEqual("", order.CountryTo);
        //        Assert.AreEqual(null, order.ExchangeRate);
        //        Assert.AreEqual("AG00SYS", order.FromAgentID);
        //        Assert.AreEqual(false, order.OnLegalHold);
        //        Assert.AreEqual("169039086", order.OrderID);
        //        Assert.AreEqual(null, order.PayDataMessage);
        //        Assert.AreEqual(Convert.ToDateTime("9/29/2014 8:29:19 PM"), order.PayDataNotAfterDate);
        //        Assert.AreEqual(true, order.PayDataNotAfterDateSpecified);
        //        Assert.AreEqual(Convert.ToDateTime("1/1/0001 12:00:00 AM"), order.PayDataNotBeforeDate);
        //        Assert.AreEqual(false, order.PayDataNotBeforeDateSpecified);
        //        Assert.AreEqual(121.00m, order.PayoutAmount.Amount);
        //        Assert.AreEqual("USD", order.PayoutAmount.CurrencyCode);
        //        Assert.AreEqual("123 Main street", order.ReceiverAddress);
        //        Assert.AreEqual("Santa Monica", order.ReceiverCity);
        //        Assert.AreEqual(0.61m, order.ReceiverComission.Amount);
        //        Assert.AreEqual("USD", order.ReceiverComission.CurrencyCode);
        //        Assert.AreEqual("US", order.ReceiverCountry);
        //        Assert.AreEqual(null, order.ReceiverEmail);
        //        Assert.AreEqual("ПЕТР", order.ReceiverFirstName);
        //        Assert.AreEqual("VAN ROOYEN WILLIAM", order.ReceiverFullName);
        //        Assert.AreEqual(null, order.ReceiverIDIssuer);
        //        Assert.AreEqual("7709225545874", order.ReceiverIDNumber);
        //        Assert.AreEqual(null, order.ReceiverIDSerialNumber);
        //        Assert.AreEqual(0, order.ReceiverIDTypeID);
        //        Assert.AreEqual("Other", order.ReceiverIDTypeText);
        //        Assert.AreEqual("0", order.ReceiverIsResident);
        //        Assert.AreEqual("КРУЖКИН", order.ReceiverLastName1);
        //        Assert.AreEqual("", order.ReceiverLastName2);
        //        Assert.AreEqual("ФОМИЧ", order.ReceiverMiddleName);
        //        Assert.AreEqual("90290", order.ReceiverPhone);
        //        Assert.AreEqual("N/A", order.ReceiverState);
        //        Assert.AreEqual(null, order.SendAmount);
        //        Assert.AreEqual("Н", order.SenderAddress);
        //        Assert.AreEqual("Н", order.SenderCity);
        //        Assert.AreEqual(0.36m, order.SenderComission.Amount);
        //        Assert.AreEqual("USD", order.SenderComission.CurrencyCode);
        //        Assert.AreEqual("RU", order.SenderCountry);
        //        Assert.AreEqual(null, order.SenderEmail);
        //        Assert.AreEqual("АНТОН", order.SenderFirstName);
        //        Assert.AreEqual("ПЕТРОВ АНТОН СЕРГЕЕВИЧ", order.SenderFullName);
        //        Assert.AreEqual("ОВД", order.SenderIDIssuer);
        //        Assert.AreEqual("800000", order.SenderIDNumber);
        //        Assert.AreEqual("4545", order.SenderIDSerialNumber);
        //        Assert.AreEqual(0, order.SenderIDTypeID);
        //        Assert.AreEqual("Passport", order.SenderIDTypeText);
        //        Assert.AreEqual("1", order.SenderIsResident);
        //        Assert.AreEqual("ПЕТРОВ", order.SenderLastName1);
        //        Assert.AreEqual("", order.SenderLastName2);
        //        Assert.AreEqual("СЕРГЕЕВИЧ", order.SenderMiddleName);
        //        Assert.AreEqual("+79100000000", order.SenderPhone);
        //        Assert.AreEqual("N/A", order.SenderState);
        //        Assert.AreEqual(Convert.ToDateTime("7/31/2014 8:29:19 PM"), order.TransferDate);
        //        Assert.AreEqual("Paid Out", order.TransferStatus);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.AreEqual(1, e.Message);
        //    }
        //}




        //[TestMethod]
        //public void GetTransactionInfo_EmptyPIN()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "";
        //        int orderID = 0;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string agentcountry = m_agentCountry;
        //        string agentstate = m_agentState;
        //        int userID = m_userID;
        //        string countryTo = "RU";
        //        string stateTo = "M";
        //        GetTransactionInfoRequest request = new GetTransactionInfoRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.RequesterInfo.AgentCountry = agentcountry;
        //        request.RequesterInfo.AgentState = agentstate;
        //        request.RequesterInfo.AppID = m_appID;
        //        request.RequesterInfo.AppObjectID = m_appObjectID;
        //        request.RequesterInfo.Locale = "PST";
        //        request.RequesterInfo.LocalTime = DateTime.Now;
        //        request.RequesterInfo.TerminalID = "DMG";
        //        request.RequesterInfo.TerminalUserID = userID.ToString();
        //        request.RequesterInfo.Timezone = "PST";
        //        request.RequesterInfo.TimezoneID = 8;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.RequesterInfo.UserID = 80912;
        //        request.RequesterInfo.UtcTime = DateTime.UtcNow;
        //        request.RequesterInfo.Version = versionNum;
        //        request.OrderID = orderID;
        //        request.OrderPIN = pin;

        //        //Call the service and get a response:
        //        GetTransactionInfoResponse response;
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
        //                response = service.GetTransactionInfo(request);
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
        //public void GetTransactionInfo_NullPIN()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = null;
        //        int orderID = 0;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string agentcountry = m_agentCountry;
        //        string agentstate = m_agentState;
        //        int userID = m_userID;
        //        string countryTo = "RU";
        //        string stateTo = "M";
        //        GetTransactionInfoRequest request = new GetTransactionInfoRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.RequesterInfo.AgentCountry = agentcountry;
        //        request.RequesterInfo.AgentState = agentstate;
        //        request.RequesterInfo.AppID = m_appID;
        //        request.RequesterInfo.AppObjectID = m_appObjectID;
        //        request.RequesterInfo.Locale = "PST";
        //        request.RequesterInfo.LocalTime = DateTime.Now;
        //        request.RequesterInfo.TerminalID = "DMG";
        //        request.RequesterInfo.TerminalUserID = userID.ToString();
        //        request.RequesterInfo.Timezone = "PST";
        //        request.RequesterInfo.TimezoneID = 8;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.RequesterInfo.UserID = 80912;
        //        request.RequesterInfo.UtcTime = DateTime.UtcNow;
        //        request.RequesterInfo.Version = versionNum;
        //        request.OrderID = orderID;
        //        request.OrderPIN = pin;

        //        //Call the service and get a response:
        //        GetTransactionInfoResponse response;
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
        //                response = service.GetTransactionInfo(request);
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
        //public void GetTransactionInfo_BadPIN()
        //{
        //    try
        //    {
        //        //Set the call details and build a request:
        //        string pin = "0055886677";
        //        int orderID = 0;
        //        int agentID = m_agentID;
        //        int agentLocID = m_agentLocID;
        //        string agentcountry = m_agentCountry;
        //        string agentstate = m_agentState;
        //        int userID = m_userID;
        //        string countryTo = "RU";
        //        string stateTo = "M";
        //        GetTransactionInfoRequest request = new GetTransactionInfoRequest();
        //        request.RequesterInfo.AgentID = agentID;
        //        request.RequesterInfo.AgentLocID = agentLocID;
        //        request.RequesterInfo.AgentCountry = agentcountry;
        //        request.RequesterInfo.AgentState = agentstate;
        //        request.RequesterInfo.AppID = m_appID;
        //        request.RequesterInfo.AppObjectID = m_appObjectID;
        //        request.RequesterInfo.Locale = "PST";
        //        request.RequesterInfo.LocalTime = DateTime.Now;
        //        request.RequesterInfo.TerminalID = "DMG";
        //        request.RequesterInfo.TerminalUserID = userID.ToString();
        //        request.RequesterInfo.Timezone = "PST";
        //        request.RequesterInfo.TimezoneID = 8;
        //        request.RequesterInfo.UserLoginID = userID;
        //        request.RequesterInfo.UserID = 80912;
        //        request.RequesterInfo.UtcTime = DateTime.UtcNow;
        //        request.RequesterInfo.Version = versionNum;
        //        request.OrderID = orderID;
        //        request.OrderPIN = pin;

        //        //Call the service and get a response:
        //        GetTransactionInfoResponse response;
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
        //                response = service.GetTransactionInfo(request);
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
