using CES.CoreApi.Payout.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace PayoutService.Test.WebAPITests
{

    [TestClass]
    class GetTransaction_Tests
    {

        [TestMethod]
        public void GetTransInfo_NullRequest()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = null;
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.NullGetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("ValidateRequest: incoming request is null", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(1001, response.ReturnInfo.ErrorCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_Valid()
        {
            string pin = "00954712416";
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual(null, response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(0, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, response.ReturnInfo.AvailableForPayout);
                //Checks a subset of all the fields.
                Assert.IsTrue(response.PersistenceID > 0);
                Assert.AreEqual(new DateTime(2016, 7, 12, 0, 0, 0), response.TransferDate);
                Assert.AreEqual("Ready for Payout", response.TransferStatus);
                Assert.AreEqual(117.00m, response.PayoutAmount.Amount);
                Assert.AreEqual("EUR", response.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", response.CountryFrom);
                Assert.AreEqual(pin, response.OrderPIN);
                Assert.AreEqual("Cash Pickup", response.DeliveryMethod);
                Assert.AreEqual(5.85m, response.BeneficiaryTax);
                Assert.AreEqual(111.15m, response.NetAmount);
                Assert.AreEqual("NOLIIK  ADELA ", response.SenderInfo.Name);
                Assert.AreEqual("NOLIIK", response.SenderInfo.FirstName);
                Assert.AreEqual("ADELA", response.SenderInfo.LastName1);
                Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.SenderInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", response.SenderInfo.City);
                Assert.AreEqual("N/A", response.SenderInfo.State);
                Assert.AreEqual("RU", response.SenderInfo.Country);
                Assert.AreEqual("889250989", response.SenderInfo.PhoneNumber);
                Assert.AreEqual("INES  MARIA ", response.BeneficiaryInfo.Name);
                Assert.AreEqual("INES", response.BeneficiaryInfo.FirstName);
                Assert.AreEqual("MARIA", response.BeneficiaryInfo.LastName1);
                Assert.AreEqual("+79139940074", response.BeneficiaryInfo.PhoneNumber);
                //Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.BeneficiaryInfo.Address);
                Assert.AreEqual(null, response.BeneficiaryInfo.Address);
                //Assert.AreEqual("NOVOSIBIRSK", response.BeneficiaryInfo.City);
                Assert.AreEqual(null, response.BeneficiaryInfo.City);
                Assert.AreEqual("N/A", response.BeneficiaryInfo.State);
                Assert.AreEqual(5000, response.ProviderInfo.ProviderID);
                Assert.AreEqual(1, response.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Golden Crown", response.ProviderInfo.ProviderName);
                Assert.AreEqual(null, response.BeneficiaryInfo.BenExternalID);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_Ria_915661076()
        {
            string pin = "915661076";

            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 33260811;
                ri.AgentLocID = 28901155;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = DateTime.Now;
                ri.UtcTime = DateTime.UtcNow;
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-mx";
                ri.Version = "1.0";
                ri.AgentCountry = "MX";
                ri.AgentState = "CHIH";
                request.RequesterInfo = ri;
                request.OrderPIN = "915661076";
                request.OrderID = 0;
                request.CountryTo = "MX";
                request.StateTo = "CHIH";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual(null, response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(0, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, response.ReturnInfo.AvailableForPayout);
                //Checks a subset of all the fields.
                Assert.IsTrue(response.PersistenceID > 0);
                Assert.AreEqual(new DateTime(2015, 12, 14, 0, 0, 0), response.TransferDate);
                Assert.AreEqual("Available for Payment", response.TransferStatus);
                Assert.AreEqual(951.34m, response.PayoutAmount.Amount);
                Assert.AreEqual("MXN", response.PayoutAmount.CurrencyCode);
                Assert.AreEqual("US", response.CountryFrom);
                Assert.AreEqual(pin, response.OrderPIN);
                Assert.AreEqual("Cash Pick-Up", response.DeliveryMethod);
                Assert.AreEqual(47.5670m, response.BeneficiaryTax);
                Assert.AreEqual(903.773m, response.NetAmount);
                Assert.AreEqual("Ben  Roethlisberger ", response.SenderInfo.Name);
                Assert.AreEqual("Ben", response.SenderInfo.FirstName);
                Assert.AreEqual("Roethlisberger", response.SenderInfo.LastName1);
                Assert.AreEqual("777 Heinz Field", response.SenderInfo.Address);
                Assert.AreEqual("Pittsburgh", response.SenderInfo.City);
                Assert.AreEqual("PA", response.SenderInfo.State);
                Assert.AreEqual("US", response.SenderInfo.Country);
                Assert.AreEqual("1-714 9996666", response.SenderInfo.PhoneNumber);
                Assert.AreEqual("HINES  WARD ", response.BeneficiaryInfo.Name);
                Assert.AreEqual("HINES", response.BeneficiaryInfo.FirstName);
                Assert.AreEqual("WARD", response.BeneficiaryInfo.LastName1);
                Assert.AreEqual("526251140504", response.BeneficiaryInfo.PhoneNumber);
                Assert.AreEqual("123 Heinz Field", response.BeneficiaryInfo.Address);
                Assert.AreEqual("CHIHUAHUA", response.BeneficiaryInfo.City);
                Assert.AreEqual("MEX", response.BeneficiaryInfo.State);
                Assert.AreEqual(5002, response.ProviderInfo.ProviderID);
                Assert.AreEqual(1, response.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Ria", response.ProviderInfo.ProviderName);
                Assert.AreEqual(null, response.BeneficiaryInfo.BenExternalID);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_Ria_OrderDoesNotExist()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 11311;
                ri.AgentLocID = 2679301;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = DateTime.Now;
                ri.UtcTime = DateTime.UtcNow;
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "en-us";
                ri.Version = "1.0";
                ri.AgentCountry = "US";
                ri.AgentState = "CA";
                request.RequesterInfo = ri;
                request.OrderPIN = "915661789";
                request.OrderID = 0;
                request.CountryTo = "MX";
                request.StateTo = "CHIH";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("The Order does not Exist", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(90, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_Ria_Canceled()
        {
            string pin = "15841493966";
            int persistenceID = 0;

            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 1065811;
                ri.AgentLocID = 163011;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = DateTime.Now;
                ri.UtcTime = DateTime.UtcNow;
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "en-us";
                ri.Version = "1.0";
                ri.AgentCountry = "MX";
                ri.AgentState = "CHIH";
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = "MX";
                request.StateTo = "CHIH";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("The Order Is Canceled.", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(99, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, response.ReturnInfo.AvailableForPayout);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_Ria_MissingLocIDPayout()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 11311;
                ri.AgentLocID = 0;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = DateTime.Now;
                ri.UtcTime = DateTime.UtcNow;
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "en-us";
                ri.Version = "1.0";
                ri.AgentCountry = "US";
                ri.AgentState = "CA";
                request.RequesterInfo = ri;
                request.OrderPIN = "915661076";
                request.OrderID = 0;
                request.CountryTo = "MX";
                request.StateTo = "CHIH";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("Missing Agent Location ID for Payout", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(13, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_NotAvailableForPayout()
        {
            string pin = "00871870610";
            int persistenceID = 1416;

            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("Paid Out", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(7, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);

                Assert.IsTrue(response.PersistenceID > 0);
                Assert.AreEqual(new DateTime(2016, 7, 12, 0, 0, 0), response.TransferDate);
                Assert.AreEqual("Paid Out", response.TransferStatus);
                Assert.AreEqual(117.00m, response.PayoutAmount.Amount);
                Assert.AreEqual("EUR", response.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", response.CountryFrom);
                Assert.AreEqual(pin, response.OrderPIN);
                Assert.AreEqual("Cash Pickup", response.DeliveryMethod);
                Assert.AreEqual(5.85m, response.BeneficiaryTax);
                Assert.AreEqual(111.15m, response.NetAmount);
                //TODO: UNIT TEST: Check List: Assert.AreEqual("", response.CustomerServiceMessages);
                //TODO: UNIT TEST: Check List: Assert.AreEqual("", response.PayoutRequiredFields);
                Assert.AreEqual("NOLIEK  ADELA ", response.SenderInfo.Name);
                Assert.AreEqual("NOLIEK", response.SenderInfo.FirstName);
                Assert.AreEqual("ADELA", response.SenderInfo.LastName1);
                Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.SenderInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", response.SenderInfo.City);
                Assert.AreEqual("N/A", response.SenderInfo.State);
                Assert.AreEqual("RU", response.SenderInfo.Country);
                Assert.AreEqual("889250989", response.SenderInfo.PhoneNumber);
                Assert.AreEqual("INES  MARIA ", response.BeneficiaryInfo.Name);
                Assert.AreEqual("INES", response.BeneficiaryInfo.FirstName);
                Assert.AreEqual("MARIA", response.BeneficiaryInfo.LastName1);
                Assert.AreEqual("+79139940074", response.BeneficiaryInfo.PhoneNumber);
                //Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.BeneficiaryInfo.Address);
                Assert.AreEqual(null, response.BeneficiaryInfo.Address);
                //Assert.AreEqual("NOVOSIBIRSK", response.BeneficiaryInfo.City);
                Assert.AreEqual(null, response.BeneficiaryInfo.City);
                Assert.AreEqual("N/A", response.BeneficiaryInfo.State);
                Assert.AreEqual(5000, response.ProviderInfo.ProviderID);
                Assert.AreEqual(1, response.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Golden Crown", response.ProviderInfo.ProviderName);

            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_PaidOut()
        {
            string pin = "00871870610";
            int persistenceID = 1416;

            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("Paid Out", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(7, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);

                Assert.IsTrue(response.PersistenceID > 0);
                Assert.AreEqual(new DateTime(2016, 7, 12, 0, 0, 0), response.TransferDate);
                Assert.AreEqual("Paid Out", response.TransferStatus);
                Assert.AreEqual(117.00m, response.PayoutAmount.Amount);
                Assert.AreEqual("EUR", response.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", response.CountryFrom);
                Assert.AreEqual(pin, response.OrderPIN);
                Assert.AreEqual("Cash Pickup", response.DeliveryMethod);
                Assert.AreEqual(5.85m, response.BeneficiaryTax);
                Assert.AreEqual(111.15m, response.NetAmount);
                //TODO: UNIT TEST: Check List: Assert.AreEqual("", response.CustomerServiceMessages);
                //TODO: UNIT TEST: Check List: Assert.AreEqual("", response.PayoutRequiredFields);
                Assert.AreEqual("NOLIEK  ADELA ", response.SenderInfo.Name);
                Assert.AreEqual("NOLIEK", response.SenderInfo.FirstName);
                Assert.AreEqual("ADELA", response.SenderInfo.LastName1);
                Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.SenderInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", response.SenderInfo.City);
                Assert.AreEqual("N/A", response.SenderInfo.State);
                Assert.AreEqual("RU", response.SenderInfo.Country);
                Assert.AreEqual("889250989", response.SenderInfo.PhoneNumber);
                Assert.AreEqual("INES  MARIA ", response.BeneficiaryInfo.Name);
                Assert.AreEqual("INES", response.BeneficiaryInfo.FirstName);
                Assert.AreEqual("MARIA", response.BeneficiaryInfo.LastName1);
                Assert.AreEqual("+79139940074", response.BeneficiaryInfo.PhoneNumber);
                //Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.BeneficiaryInfo.Address);
                Assert.AreEqual(null, response.BeneficiaryInfo.Address);
                //Assert.AreEqual("NOVOSIBIRSK", response.BeneficiaryInfo.City);
                Assert.AreEqual(null, response.BeneficiaryInfo.City);
                Assert.AreEqual("N/A", response.BeneficiaryInfo.State);
                Assert.AreEqual(5000, response.ProviderInfo.ProviderID);
                Assert.AreEqual(1, response.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Golden Crown", response.ProviderInfo.ProviderName);

            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_NoOrderFound()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = "00714644257";
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("Provider: GC:GoldenCrown did not return any orders for the PIN.", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(9019, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_Expired()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = "00695852149";
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("Expired", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(-1, response.ReturnInfo.ErrorCode);
                Assert.AreEqual("Expired", response.TransferStatus);
                Assert.AreEqual(510.00m, response.PayoutAmount.Amount);
                Assert.AreEqual("EUR", response.PayoutAmount.CurrencyCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_00714644257()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = "00695852149";
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("Expired", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(-1, response.ReturnInfo.ErrorCode);
                Assert.AreEqual("Expired", response.TransferStatus);
                Assert.AreEqual(510.00m, response.PayoutAmount.Amount);
                Assert.AreEqual("EUR", response.PayoutAmount.CurrencyCode);
                Assert.IsTrue(response.PersistenceID > 0);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_MissingCountryTo()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = "00695852149";
                request.OrderID = 0;
                request.CountryTo = "";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("missing CountryTo.", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(1006, response.ReturnInfo.ErrorCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_NoAgentID()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                //ri.AgentID = 44729311;
                //ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = "00100582424";
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("missing AgentID.", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(1001, response.ReturnInfo.ErrorCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetTransInfo_GC_ValidFullResp()
        {
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = 0;
                RequesterInfo ri = new RequesterInfo();
                ri.AppID = 501;
                ri.AppObjectID = 100;
                ri.AgentID = 44729311;
                ri.AgentLocID = 59347511;
                ri.UserID = 1;
                ri.UserLoginID = 1;
                ri.TerminalID = "0";
                ri.TerminalUserID = "0";
                ri.LocalTime = new DateTime(2016, 06, 28, 09, 33, 30);
                ri.UtcTime = new DateTime(2016, 06, 28, 17, 33, 30);
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                request.RequesterInfo = ri;
                request.OrderPIN = "00596488676";
                request.OrderID = 0;
                request.CountryTo = "ES";
                request.StateTo = "RM";
                //Make the Call:
                GetTransactionInfoResponse response = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual(null, response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(99, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, response.ReturnInfo.AvailableForPayout);

                Assert.IsTrue(response.PersistenceID > 0);
                Assert.AreEqual(null, response.OrderID);
                Assert.AreEqual(new DateTime(2016, 5, 10, 0, 0, 0), response.TransferDate);
                Assert.AreEqual("Ready for Payout", response.TransferStatus);
                Assert.AreEqual(null, response.SendAmount);
                Assert.AreEqual(110.00m, response.PayoutAmount.Amount);
                Assert.AreEqual("EUR", response.PayoutAmount.CurrencyCode);
                Assert.AreEqual(null, response.ExchangeRate);
                Assert.AreEqual(null, response.Comission);
                Assert.AreEqual(null, response.ReceiverComission);
                Assert.AreEqual(null, response.SenderComission);
                Assert.AreEqual(null, response.RecAgentID);
                Assert.AreEqual("RU", response.CountryFrom);
                Assert.AreEqual(null, response.CountryTo);
                Assert.AreEqual(null, response.PayDataMessage);
                Assert.AreEqual(null, response.SenderIsResident);
                Assert.AreEqual(null, response.ReceiverIsResident);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.PayDataNotAfterDate);
                Assert.AreEqual(false, response.PayDataNotAfterDateSpecified);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.PayDataNotBeforeDate);
                Assert.AreEqual(false, response.PayDataNotBeforeDateSpecified);
                Assert.AreEqual(false, response.OnLegalHold);
                Assert.AreEqual(null, response.ReceiverFullCountryName);
                Assert.AreEqual("00596488676", response.OrderPIN);
                Assert.AreEqual(null, response.PASeqID);
                Assert.AreEqual(null, response.PayAgent);
                Assert.AreEqual(null, response.PayAgentBranchName);
                Assert.AreEqual(null, response.PayAgentBranchNo);
                Assert.AreEqual("Cash Pickup", response.DeliveryMethod);
                Assert.AreEqual(5.5m, response.BeneficiaryTax);
                Assert.AreEqual(104.5m, response.NetAmount);
                //TODO: List CustomerServiceMessages[List<CustomerServiceMessages>]
                //TODO: List PayoutRequiredFields[List<PayoutFields>]
                Assert.AreEqual(0, response.SenderInfo.CustomerInternalID);
                Assert.AreEqual(null, response.SenderInfo.CustomerExternalID);
                Assert.AreEqual("NOLIAJ  ADELA ", response.SenderInfo.Name);
                Assert.AreEqual("NOLIAJ", response.SenderInfo.FirstName);
                Assert.AreEqual("ADELA", response.SenderInfo.LastName1);
                Assert.AreEqual(null, response.SenderInfo.LastName2);
                Assert.AreEqual(null, response.SenderInfo.MiddleName);
                Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.SenderInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", response.SenderInfo.City);
                Assert.AreEqual(null, response.SenderInfo.CityID);
                Assert.AreEqual(null, response.SenderInfo.District);
                Assert.AreEqual(null, response.SenderInfo.County);
                Assert.AreEqual("N/A", response.SenderInfo.State);
                Assert.AreEqual(null, response.SenderInfo.StateID);
                Assert.AreEqual("RU", response.SenderInfo.Country);
                Assert.AreEqual(null, response.SenderInfo.PostalCode);
                Assert.AreEqual("889250989", response.SenderInfo.PhoneNumber);
                Assert.AreEqual(null, response.SenderInfo.EmailAddress);
                Assert.AreEqual(null, response.SenderInfo.Gender);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.SenderInfo.DateOfBirth);
                Assert.AreEqual(null, response.SenderInfo.Nationality);
                Assert.AreEqual(null, response.SenderInfo.CountryOfBirth);
                Assert.AreEqual(null, response.SenderInfo.BirthStateID);
                Assert.AreEqual(null, response.SenderInfo.BirthCityID);
                Assert.AreEqual(null, response.SenderInfo.BirthCity);
                Assert.AreEqual(null, response.SenderInfo.CountryOfResidence);
                Assert.AreEqual(null, response.SenderInfo.TaxID);
                Assert.AreEqual(false, response.SenderInfo.DoesNotHaveATaxID);
                Assert.AreEqual(null, response.SenderInfo.Curp);
                Assert.AreEqual(null, response.SenderInfo.Occupation);
                Assert.AreEqual(0, response.SenderInfo.OccupationID);
                Assert.AreEqual(null, response.SenderInfo.IDType);
                Assert.AreEqual(0, response.SenderInfo.IDTypeID);
                Assert.AreEqual(null, response.SenderInfo.IDNumber);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.SenderInfo.IDExpDate);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.SenderInfo.IDIssuedDate);
                Assert.AreEqual(null, response.SenderInfo.IDIssuedByCountry);
                Assert.AreEqual(null, response.SenderInfo.IDIssuedByState);
                Assert.AreEqual(0, response.SenderInfo.IDIssuedByStateID);
                Assert.AreEqual(null, response.SenderInfo.IDIssuedBy);
                Assert.AreEqual(null, response.SenderInfo.IDSerialNumber);
                Assert.AreEqual(0, response.BeneficiaryInfo.BenInternalID);
                Assert.AreEqual(null, response.BeneficiaryInfo.BenExternalID);
                Assert.AreEqual(null, response.BeneficiaryInfo.SourceOfFunds);
                Assert.AreEqual(null, response.BeneficiaryInfo.Gender);
                Assert.AreEqual(null, response.BeneficiaryInfo.TaxCountry);
                Assert.AreEqual("INES  MARIA ", response.BeneficiaryInfo.Name);
                Assert.AreEqual("INES", response.BeneficiaryInfo.FirstName);
                Assert.AreEqual("MARIA", response.BeneficiaryInfo.LastName1);
                Assert.AreEqual(null, response.BeneficiaryInfo.LastName2);
                Assert.AreEqual(null, response.BeneficiaryInfo.MiddleName);
                Assert.AreEqual(null, response.BeneficiaryInfo.IDType);
                Assert.AreEqual(0, response.BeneficiaryInfo.IDTypeID);
                Assert.AreEqual(null, response.BeneficiaryInfo.IDNumber);
                Assert.AreEqual(null, response.BeneficiaryInfo.IDSerialNumber);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.BeneficiaryInfo.IDExpDate);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.BeneficiaryInfo.IDIssuedDate);
                Assert.AreEqual(null, response.BeneficiaryInfo.IDIssuedByCountry);
                Assert.AreEqual(null, response.BeneficiaryInfo.IDIssuedByState);
                Assert.AreEqual(0, response.BeneficiaryInfo.IDIssuedByStateID);
                Assert.AreEqual(null, response.BeneficiaryInfo.IDIssuedBy);
                Assert.AreEqual(null, response.BeneficiaryInfo.IDIssuer);
                Assert.AreEqual("+79139940074", response.BeneficiaryInfo.PhoneNumber);
                Assert.AreEqual(null, response.BeneficiaryInfo.EmailAddress);
                Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.BeneficiaryInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", response.BeneficiaryInfo.City);
                Assert.AreEqual(null, response.BeneficiaryInfo.CityID);
                Assert.AreEqual(null, response.BeneficiaryInfo.Country);
                Assert.AreEqual(null, response.BeneficiaryInfo.District);
                Assert.AreEqual(null, response.BeneficiaryInfo.County);
                Assert.AreEqual("N/A", response.BeneficiaryInfo.State);
                Assert.AreEqual(null, response.BeneficiaryInfo.StateID);
                Assert.AreEqual(null, response.BeneficiaryInfo.PostalCode);
                Assert.AreEqual(new DateTime(0001, 01, 01, 0, 0, 0), response.BeneficiaryInfo.DateOfBirth);
                Assert.AreEqual(null, response.BeneficiaryInfo.Nationality);
                Assert.AreEqual(null, response.BeneficiaryInfo.CountryOfBirth);
                Assert.AreEqual(null, response.BeneficiaryInfo.BirthStateID);
                Assert.AreEqual(null, response.BeneficiaryInfo.BirthCityID);
                Assert.AreEqual(null, response.BeneficiaryInfo.BirthCity);
                Assert.AreEqual(null, response.BeneficiaryInfo.CountryOfResidence);
                Assert.AreEqual(null, response.BeneficiaryInfo.TaxID);
                Assert.AreEqual(null, response.BeneficiaryInfo.Occupation);
                Assert.AreEqual(0, response.BeneficiaryInfo.OccupationID);
                Assert.AreEqual(null, response.BeneficiaryInfo.Curp);
                Assert.AreEqual(0, response.BeneficiaryInfo.TaxAmount);
                Assert.AreEqual(false, response.BeneficiaryInfo.DoesNotHaveATaxID);
                Assert.AreEqual(0, response.BeneficiaryInfo.CustRelationshipID);
                Assert.AreEqual(null, response.BeneficiaryInfo.CustRelationship);
                Assert.AreEqual(5000, response.ProviderInfo.ProviderID);
                Assert.AreEqual(1, response.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Golden Crown", response.ProviderInfo.ProviderName);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

    }
}
