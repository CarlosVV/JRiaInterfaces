using CES.CoreApi.Payout.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace PayoutService.Test.WebAPITests
{

    [TestClass]
    class PayoutTransaction_Tests
    {

        [TestMethod]
        public void PayTrans_GC_Valid()
        {
            string pin = "00954712416";
            int persistenceID = 1416;
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
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
                ri.LocalTime = DateTime.Now;
                ri.UtcTime = DateTime.UtcNow;
                ri.Timezone = "-3";
                ri.TimezoneID = 8701;
                ri.Locale = "es-cl";
                ri.Version = "1.0";
                ri.AgentCountry = "ES";
                ri.AgentState = "RM";
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = "DMG Test";
                b.Gender = "M";
                b.TaxCountry = "";
                b.Name = "MARIA INES";
                b.FirstName = "INES";
                b.LastName1 = "MARIA";
                b.LastName2 = "ISLA";
                b.MiddleName = "M";
                b.IDType = "Passport";
                b.IDTypeID = 0;
                b.IDNumber = "123456789";
                b.IDSerialNumber = "";
                b.IDExpDate = new DateTime(2020, 04, 26);
                b.IDIssuedDate = new DateTime(2015, 04, 26);
                b.IDIssuedByCountry = "ES";
                b.IDIssuedByState = "M";
                b.IDIssuedByStateID = 0;
                b.IDIssuedBy = "Goverment";
                b.IDIssuer = "";
                b.PhoneNumber = "74081860";
                b.EmailAddress = "";
                b.Address = "123 Main St";
                b.City = "Madrid";
                b.CityID = -1;
                b.Country = "ES";
                b.District = "";
                b.County = "";
                b.State = "Madrid";
                b.StateID = -1;
                b.PostalCode = "44444";
                b.DateOfBirth = new DateTime(1979, 09, 09); 
                b.Nationality = "ES";
                b.CountryOfBirth = "1";
                b.BirthStateID = 0;
                b.BirthCityID = 0;
                b.BirthCity = "Santiago";
                b.CountryOfResidence = "";
                b.TaxID = "";
                b.Occupation = "";
                b.OccupationID = 0;
                b.Curp = "";
                b.TaxAmount = 0.00m;
                b.DoesNotHaveATaxID = true;
                b.CustRelationshipID = 0;
                b.CustRelationship = "";
                SenderInfo s = new SenderInfo();
                s.Address = "C RONDA DE PONIENTE 11 - 2D";
                s.BirthCity = "NOVOSIBIRSK";
                s.BirthCityID = 0;
                s.BirthStateID = 0;
                s.City = "NOVOSIBIRSK";
                s.CityID = 0;
                s.Country = "RU";
                s.CountryOfBirth = "RU";
                s.CountryOfResidence = "RU";
                s.County = "NA";
                s.Curp = "";
                s.CustomerExternalID = "0";
                s.CustomerInternalID = 0;
                s.DateOfBirth = new DateTime(1980, 05, 05); 
                s.District = "";
                s.DoesNotHaveATaxID = false;
                s.FirstName = "NOLIEK";
                s.Gender = "M";
                s.IDExpDate = new DateTime(2018, 05, 05);
                s.IDIssuedBy = "RU";
                s.IDIssuedByCountry = "RU";
                s.IDIssuedByState = "RU";
                s.IDIssuedByStateID = 0;
                s.IDIssuedDate = new DateTime(2010, 05, 05);
                s.IDNumber = "33226655";
                s.IDType = "Passport";
                s.IDTypeID = 205;
                s.LastName1 = "ADELA";
                s.LastName2 = "NOLIEK";
                s.MiddleName = "M";
                s.Name = "ADELA NOLIEK";
                s.Nationality = "RU";
                s.Occupation = "None";
                s.OccupationID = 0;
                s.PhoneNumber = "889250989";
                s.PostalCode = "22222";
                s.State = "M";
                s.StateID = 0;
                s.TaxID = "123114444";
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;
                request.TellerDrawerInstanceID = 2;
                request.OrderID = 0;
                request.OrderPIN = pin;
                request.OrderLookupCode = pin;
                request.RecAgentID = 7884114;
                request.RecAgentLocID = 0;
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "Moscow";
                request.RecAgentState = "M";
                request.RecAgentPostalCode = "11111";
                request.RecAgentCountry = "RU";
                request.PayAgentID = 44729311;
                request.PayAgentLocID = 59347511;
                request.PayAgentBranch = "";
                request.PayAgentAddress = "123 Main";
                request.PayAgentCity = "Madrid";
                request.PayAgentState = "M";
                request.PayAgentPostalCode = "22222";
                request.PayAgentCountry = "ES";
                request.PayAgentCountryID = 0;
                request.SendCurrency = "EUR";
                request.SendAmount = 510.00m;
                request.SendCharge = 10.00m;
                request.PayoutCurrency = "EUR";
                request.PayoutAmount = 510.00m;
                request.PayAgentCountryID = 0;
                request.PayoutMethodID = 0;
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = 0;
                request.TransferReasonID = 0;
                request.TransferReason = "";
                request.ApproverID = 0;
                request.ConvertedCurrency = "";
                request.ConvertedRate = 1.0m;
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse response = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual("Paid Successful", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(0, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, response.ReturnInfo.AvailableForPayout);
                Assert.AreEqual("", response.ConfirmationNumber);
                Assert.AreEqual("", response.BeneficiaryFee.Amount);
                Assert.AreEqual("EUR", response.BeneficiaryFee.CurrencyCode);
                Assert.AreEqual(persistenceID, response.PersistenceID);
                Assert.AreEqual("", response.BeneficiaryPayout.Amount);
                Assert.AreEqual("EUR", response.BeneficiaryPayout.CurrencyCode);
                Assert.AreEqual(true, response.IsValid);
                Assert.AreEqual(0, response.OrderID);
                Assert.AreEqual("", response.OrderStatus);
                Assert.AreEqual("", response.PayoutCommission.Amount);
                Assert.AreEqual("", response.PayoutCommission.CurrencyCode);
                Assert.AreEqual(pin, response.PIN);
                Assert.AreEqual(DateTime.UtcNow, response.ResponseDateTimeUTC);
                if (response.PayoutRequiredFields != null)
                {
                    foreach(PayoutFields f in response.PayoutRequiredFields)
                    {
                        Assert.AreEqual("", f.FieldName);
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void PayTrans_GC_TooFarInPast()
        {
            string pin = "00871870610";
            int persistenceID = 1416;

            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
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
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = "DMG Test";
                b.Gender = "M";
                b.TaxCountry = "";
                b.Name = "MARIA INES";
                b.FirstName = "MARIA";
                b.LastName1 = "INES";
                b.LastName2 = "ISLA";
                b.MiddleName = "M";
                b.IDType = "Passport";
                b.IDTypeID = 0;
                b.IDNumber = "123456789";
                b.IDSerialNumber = "";
                b.IDExpDate = new DateTime(2020, 04, 26);
                b.IDIssuedDate = new DateTime(2015, 04, 26);
                b.IDIssuedByCountry = "ES";
                b.IDIssuedByState = "M";
                b.IDIssuedByStateID = 0;
                b.IDIssuedBy = "Goverment";
                b.IDIssuer = "";
                b.PhoneNumber = "74081860";
                b.EmailAddress = "";
                b.Address = "123 Main St";
                b.City = "Madrid";
                b.CityID = -1;
                b.Country = "ES";
                b.District = "";
                b.County = "";
                b.State = "Madrid";
                b.StateID = -1;
                b.PostalCode = "44444";
                b.DateOfBirth = new DateTime(1979, 09, 09);
                b.Nationality = "ES";
                b.CountryOfBirth = "1";
                b.BirthStateID = 0;
                b.BirthCityID = 0;
                b.BirthCity = "Santiago";
                b.CountryOfResidence = "";
                b.TaxID = "";
                b.Occupation = "";
                b.OccupationID = 0;
                b.Curp = "";
                b.TaxAmount = 0.00m;
                b.DoesNotHaveATaxID = true;
                b.CustRelationshipID = 0;
                b.CustRelationship = "";
                SenderInfo s = new SenderInfo();
                s.Address = "C RONDA DE PONIENTE 11 - 2D";
                s.BirthCity = "NOVOSIBIRSK";
                s.BirthCityID = 0;
                s.BirthStateID = 0;
                s.City = "NOVOSIBIRSK";
                s.CityID = 0;
                s.Country = "RU";
                s.CountryOfBirth = "RU";
                s.CountryOfResidence = "RU";
                s.County = "NA";
                s.Curp = "";
                s.CustomerExternalID = "0";
                s.CustomerInternalID = 0;
                s.DateOfBirth = new DateTime(1980, 05, 05);
                s.District = "";
                s.DoesNotHaveATaxID = false;
                s.FirstName = "NOLYAB";
                s.Gender = "M";
                s.IDExpDate = new DateTime(2018, 05, 05);
                s.IDIssuedBy = "RU";
                s.IDIssuedByCountry = "RU";
                s.IDIssuedByState = "RU";
                s.IDIssuedByStateID = 0;
                s.IDIssuedDate = new DateTime(2010, 05, 05);
                s.IDNumber = "33226655";
                s.IDType = "Passport";
                s.IDTypeID = 205;
                s.LastName1 = "ADELA";
                s.LastName2 = "NOVAKOV";
                s.MiddleName = "M";
                s.Name = "ADELA NOLYAB";
                s.Nationality = "RU";
                s.Occupation = "None";
                s.OccupationID = 0;
                s.PhoneNumber = "889250989";
                s.PostalCode = "22222";
                s.State = "M";
                s.StateID = 0;
                s.TaxID = "123114444";
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;
                request.TellerDrawerInstanceID = 2;
                request.OrderID = 0;
                request.OrderPIN = pin;
                request.OrderLookupCode = pin;
                request.RecAgentID = 7884114;
                request.RecAgentLocID = 0;
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "Moscow";
                request.RecAgentState = "M";
                request.RecAgentPostalCode = "11111";
                request.RecAgentCountry = "RU";
                request.PayAgentID = 44729311;
                request.PayAgentLocID = 59347511;
                request.PayAgentBranch = "";
                request.PayAgentAddress = "123 Main";
                request.PayAgentCity = "Madrid";
                request.PayAgentState = "M";
                request.PayAgentPostalCode = "22222";
                request.PayAgentCountry = "ES";
                request.PayAgentCountryID = 0;
                request.SendCurrency = "EUR";
                request.SendAmount = 510.00m;
                request.SendCharge = 10.00m;
                request.PayoutCurrency = "EUR";
                request.PayoutAmount = 510.00m;
                request.PayAgentCountryID = 0;
                request.PayoutMethodID = 0;
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = 0;
                request.TransferReasonID = 0;
                request.TransferReason = "";
                request.ApproverID = 0;
                request.ConvertedCurrency = "";
                request.ConvertedRate = 1.0m;
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse response = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual("Payout Local Time is not valid (too far in the past).", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(21, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
                Assert.AreEqual(null, response.ConfirmationNumber);
                Assert.AreEqual(0.0m, response.BeneficiaryFee.Amount);
                Assert.AreEqual(null, response.BeneficiaryFee.CurrencyCode);
                Assert.AreEqual(1416, response.PersistenceID);
                Assert.AreEqual("", response.BeneficiaryPayout.Amount);
                Assert.AreEqual("EUR", response.BeneficiaryPayout.CurrencyCode);
                Assert.AreEqual(true, response.IsValid);
                Assert.AreEqual(0, response.OrderID);
                Assert.AreEqual("", response.OrderStatus);
                Assert.AreEqual("", response.PayoutCommission.Amount);
                Assert.AreEqual("", response.PayoutCommission.CurrencyCode);
                Assert.AreEqual(pin, response.PIN);
                Assert.AreEqual(DateTime.UtcNow, response.ResponseDateTimeUTC);
                if (response.PayoutRequiredFields != null)
                {
                    foreach (PayoutFields f in response.PayoutRequiredFields)
                    {
                        Assert.AreEqual("", f.FieldName);
                    }
                }
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void PayTrans_MissingPersistenceID()
        {
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
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
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = "DMG Test";
                b.Gender = "M";
                b.TaxCountry = "";
                b.Name = "MARIA INES";
                b.FirstName = "MARIA";
                b.LastName1 = "INES";
                b.LastName2 = "ISLA";
                b.MiddleName = "M";
                b.IDType = "Passport";
                b.IDTypeID = 0;
                b.IDNumber = "123456789";
                b.IDSerialNumber = "";
                b.IDExpDate = new DateTime(2020, 04, 26);
                b.IDIssuedDate = new DateTime(2015, 04, 26);
                b.IDIssuedByCountry = "ES";
                b.IDIssuedByState = "M";
                b.IDIssuedByStateID = 0;
                b.IDIssuedBy = "Goverment";
                b.IDIssuer = "";
                b.PhoneNumber = "74081860";
                b.EmailAddress = "";
                b.Address = "123 Main St";
                b.City = "Madrid";
                b.CityID = 0;
                b.Country = "ES";
                b.District = "";
                b.County = "";
                b.State = "Madrid";
                b.StateID = 0;
                b.PostalCode = "44444";
                b.DateOfBirth = new DateTime(1979, 09, 09);
                b.Nationality = "ES";
                b.CountryOfBirth = "1";
                b.BirthStateID = 0;
                b.BirthCityID = 0;
                b.BirthCity = "Santiago";
                b.CountryOfResidence = "";
                b.TaxID = "";
                b.Occupation = "";
                b.OccupationID = 0;
                b.Curp = "";
                b.TaxAmount = 0.00m;
                b.DoesNotHaveATaxID = true;
                b.CustRelationshipID = 0;
                b.CustRelationship = "";
                SenderInfo s = new SenderInfo();
                s.Address = "C RONDA DE PONIENTE 11 - 2D";
                s.BirthCity = "NOVOSIBIRSK";
                s.BirthCityID = 0;
                s.BirthStateID = 0;
                s.City = "NOVOSIBIRSK";
                s.CityID = 0;
                s.Country = "RU";
                s.CountryOfBirth = "RU";
                s.CountryOfResidence = "RU";
                s.County = "NA";
                s.Curp = "";
                s.CustomerExternalID = "0";
                s.CustomerInternalID = 0;
                s.DateOfBirth = new DateTime(1980, 05, 05);
                s.District = "";
                s.DoesNotHaveATaxID = false;
                s.FirstName = "NOLYAB";
                s.Gender = "M";
                s.IDExpDate = new DateTime(2018, 05, 05);
                s.IDIssuedBy = "RU";
                s.IDIssuedByCountry = "RU";
                s.IDIssuedByState = "RU";
                s.IDIssuedByStateID = 0;
                s.IDIssuedDate = new DateTime(2010, 05, 05);
                s.IDNumber = "33226655";
                s.IDType = "Passport";
                s.IDTypeID = 205;
                s.LastName1 = "ADELA";
                s.LastName2 = "NOVAKOV";
                s.MiddleName = "M";
                s.Name = "ADELA NOLYAB";
                s.Nationality = "RU";
                s.Occupation = "None";
                s.OccupationID = 0;
                s.PhoneNumber = "889250989";
                s.PostalCode = "22222";
                s.State = "M";
                s.StateID = 0;
                s.TaxID = "123114444";
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;
                request.TellerDrawerInstanceID = 2;
                request.OrderID = 0;
                request.OrderPIN = "00714644257";
                request.OrderLookupCode = "00714644257";
                request.RecAgentID = 7884114;
                request.RecAgentLocID = 0;
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "Moscow";
                request.RecAgentState = "M";
                request.RecAgentPostalCode = "11111";
                request.RecAgentCountry = "RU";
                request.PayAgentID = 44729311;
                request.PayAgentLocID = 59347511;
                request.PayAgentBranch = "";
                request.PayAgentAddress = "123 Main";
                request.PayAgentCity = "Madrid";
                request.PayAgentState = "M";
                request.PayAgentPostalCode = "22222";
                request.PayAgentCountry = "ES";
                request.PayAgentCountryID = 0;
                request.SendCurrency = "EUR";
                request.SendAmount = 510.00m;
                request.SendCharge = 10.00m;
                request.PayoutCurrency = "EUR";
                request.PayoutAmount = 510.00m;
                request.PayAgentCountryID = 0;
                request.PayoutMethodID = 0;
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = 0;
                request.TransferReasonID = 0;
                request.TransferReason = "";
                request.ApproverID = 0;
                request.ConvertedCurrency = "";
                request.ConvertedRate = 1.0m;
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse response = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual("missing PersistenceID.", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(2015, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void PayTrans_GC_OrderNotAvailPayout()
        {
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
                request.PersistenceID = 1296;
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
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = "DMG Test";
                b.Gender = "M";
                b.TaxCountry = "";
                b.Name = "MARIA INES";
                b.FirstName = "MARIA";
                b.LastName1 = "INES";
                b.LastName2 = "ISLA";
                b.MiddleName = "M";
                b.IDType = "Passport";
                b.IDTypeID = 0;
                b.IDNumber = "123456789";
                b.IDSerialNumber = "";
                b.IDExpDate = new DateTime(2020, 04, 26);
                b.IDIssuedDate = new DateTime(2015, 04, 26);
                b.IDIssuedByCountry = "ES";
                b.IDIssuedByState = "M";
                b.IDIssuedByStateID = 0;
                b.IDIssuedBy = "Goverment";
                b.IDIssuer = "";
                b.PhoneNumber = "74081860";
                b.EmailAddress = "";
                b.Address = "123 Main St";
                b.City = "Madrid";
                b.CityID = -1;
                b.Country = "ES";
                b.District = "";
                b.County = "";
                b.State = "Madrid";
                b.StateID = -1;
                b.PostalCode = "44444";
                b.DateOfBirth = new DateTime(1979, 09, 09);
                b.Nationality = "ES";
                b.CountryOfBirth = "1";
                b.BirthStateID = 0;
                b.BirthCityID = 0;
                b.BirthCity = "Santiago";
                b.CountryOfResidence = "";
                b.TaxID = "";
                b.Occupation = "";
                b.OccupationID = 0;
                b.Curp = "";
                b.TaxAmount = 0.00m;
                b.DoesNotHaveATaxID = true;
                b.CustRelationshipID = 0;
                b.CustRelationship = "";
                SenderInfo s = new SenderInfo();
                s.Address = "C RONDA DE PONIENTE 11 - 2D";
                s.BirthCity = "NOVOSIBIRSK";
                s.BirthCityID = 0;
                s.BirthStateID = 0;
                s.City = "NOVOSIBIRSK";
                s.CityID = 0;
                s.Country = "RU";
                s.CountryOfBirth = "RU";
                s.CountryOfResidence = "RU";
                s.County = "NA";
                s.Curp = "";
                s.CustomerExternalID = "0";
                s.CustomerInternalID = 0;
                s.DateOfBirth = new DateTime(1980, 05, 05);
                s.District = "";
                s.DoesNotHaveATaxID = false;
                s.FirstName = "NOLYAB";
                s.Gender = "M";
                s.IDExpDate = new DateTime(2018, 05, 05);
                s.IDIssuedBy = "RU";
                s.IDIssuedByCountry = "RU";
                s.IDIssuedByState = "RU";
                s.IDIssuedByStateID = 0;
                s.IDIssuedDate = new DateTime(2010, 05, 05);
                s.IDNumber = "33226655";
                s.IDType = "Passport";
                s.IDTypeID = 205;
                s.LastName1 = "ADELA";
                s.LastName2 = "NOVAKOV";
                s.MiddleName = "M";
                s.Name = "ADELA NOLYAB";
                s.Nationality = "RU";
                s.Occupation = "None";
                s.OccupationID = 0;
                s.PhoneNumber = "889250989";
                s.PostalCode = "22222";
                s.State = "M";
                s.StateID = 0;
                s.TaxID = "123114444";
                
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;
                
                request.TellerDrawerInstanceID = 2;
                request.OrderID = 0;
                request.OrderPIN = "00714644257";
                request.OrderLookupCode = "00714644257";
                request.RecAgentID = 7884114;
                request.RecAgentLocID = 0;
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "Moscow";
                request.RecAgentState = "M";
                request.RecAgentPostalCode = "11111";
                request.RecAgentCountry = "RU";
                request.PayAgentID = 44729311;
                request.PayAgentLocID = 59347511;
                request.PayAgentBranch = "";
                request.PayAgentAddress = "123 Main";
                request.PayAgentCity = "Madrid";
                request.PayAgentState = "M";
                request.PayAgentPostalCode = "22222";
                request.PayAgentCountry = "ES";
                request.PayAgentCountryID = 0;
                request.SendCurrency = "EUR";
                request.SendAmount = 510.00m;
                request.SendCharge = 10.00m;
                request.PayoutCurrency = "EUR";
                request.PayoutAmount = 510.00m;
                request.PayAgentCountryID = 0;
                request.PayoutMethodID = 0;
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = 0;
                request.TransferReasonID = 0;
                request.TransferReason = "";
                request.ApproverID = 0;
                request.ConvertedCurrency = "";
                request.ConvertedRate = 1.0m;
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse response = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual("Payout Service: Order not available", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(9010, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void PayTrans_PersistenceDoesNotExist()
        {
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
                request.PersistenceID = 25348;
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
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = "DMG Test";
                b.Gender = "M";
                b.TaxCountry = "";
                b.Name = "MARIA INES";
                b.FirstName = "MARIA";
                b.LastName1 = "INES";
                b.LastName2 = "ISLA";
                b.MiddleName = "M";
                b.IDType = "Passport";
                b.IDTypeID = 0;
                b.IDNumber = "123456789";
                b.IDSerialNumber = "";
                b.IDExpDate = new DateTime(2020, 04, 26);
                b.IDIssuedDate = new DateTime(2015, 04, 26);
                b.IDIssuedByCountry = "ES";
                b.IDIssuedByState = "M";
                b.IDIssuedByStateID = 0;
                b.IDIssuedBy = "Goverment";
                b.IDIssuer = "";
                b.PhoneNumber = "74081860";
                b.EmailAddress = "";
                b.Address = "123 Main St";
                b.City = "Madrid";
                b.CityID = 0;
                b.Country = "ES";
                b.District = "";
                b.County = "";
                b.State = "Madrid";
                b.StateID = 0;
                b.PostalCode = "44444";
                b.DateOfBirth = new DateTime(1979, 09, 09);
                b.Nationality = "ES";
                b.CountryOfBirth = "1";
                b.BirthStateID = 0;
                b.BirthCityID = 0;
                b.BirthCity = "Santiago";
                b.CountryOfResidence = "";
                b.TaxID = "";
                b.Occupation = "";
                b.OccupationID = 0;
                b.Curp = "";
                b.TaxAmount = 0.00m;
                b.DoesNotHaveATaxID = true;
                b.CustRelationshipID = 0;
                b.CustRelationship = "";
                SenderInfo s = new SenderInfo();
                s.Address = "C RONDA DE PONIENTE 11 - 2D";
                s.BirthCity = "NOVOSIBIRSK";
                s.BirthCityID = 0;
                s.BirthStateID = 0;
                s.City = "NOVOSIBIRSK";
                s.CityID = 0;
                s.Country = "RU";
                s.CountryOfBirth = "RU";
                s.CountryOfResidence = "RU";
                s.County = "NA";
                s.Curp = "";
                s.CustomerExternalID = "0";
                s.CustomerInternalID = 0;
                s.DateOfBirth = new DateTime(1980, 05, 05);
                s.District = "";
                s.DoesNotHaveATaxID = false;
                s.FirstName = "NOLYAB";
                s.Gender = "M";
                s.IDExpDate = new DateTime(2018, 05, 05);
                s.IDIssuedBy = "RU";
                s.IDIssuedByCountry = "RU";
                s.IDIssuedByState = "RU";
                s.IDIssuedByStateID = 0;
                s.IDIssuedDate = new DateTime(2010, 05, 05);
                s.IDNumber = "33226655";
                s.IDType = "Passport";
                s.IDTypeID = 205;
                s.LastName1 = "ADELA";
                s.LastName2 = "NOVAKOV";
                s.MiddleName = "M";
                s.Name = "ADELA NOLYAB";
                s.Nationality = "RU";
                s.Occupation = "None";
                s.OccupationID = 0;
                s.PhoneNumber = "889250989";
                s.PostalCode = "22222";
                s.State = "M";
                s.StateID = 0;
                s.TaxID = "123114444";
                
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;
                
                request.TellerDrawerInstanceID = 2;
                request.OrderID = 0;
                request.OrderPIN = "00714644257";
                request.OrderLookupCode = "00714644257";
                request.RecAgentID = 7884114;
                request.RecAgentLocID = 0;
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "Moscow";
                request.RecAgentState = "M";
                request.RecAgentPostalCode = "11111";
                request.RecAgentCountry = "RU";
                request.PayAgentID = 44729311;
                request.PayAgentLocID = 59347511;
                request.PayAgentBranch = "";
                request.PayAgentAddress = "123 Main";
                request.PayAgentCity = "Madrid";
                request.PayAgentState = "M";
                request.PayAgentPostalCode = "22222";
                request.PayAgentCountry = "ES";
                request.PayAgentCountryID = 0;
                request.SendCurrency = "EUR";
                request.SendAmount = 510.00m;
                request.SendCharge = 10.00m;
                request.PayoutCurrency = "EUR";
                request.PayoutAmount = 510.00m;
                request.PayAgentCountryID = 0;
                request.PayoutMethodID = 0;
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = 0;
                request.TransferReasonID = 0;
                request.TransferReason = "";
                request.ApproverID = 0;
                request.ConvertedCurrency = "";
                request.ConvertedRate = 1.0m;
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse response = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual("Payout Service: Persistence number does not exist", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(9010, response.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }


        [TestMethod]
        public void PayTrans_Ria_915661076_Paid()
        {
            string pin = "915661076";
            int persistenceID = 1336;
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
                request.PersistenceID = persistenceID;
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
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = "DMG Test";
                b.Gender = "M";
                b.TaxCountry = "";
                b.Name = "HINES WARD";
                b.FirstName = "HINES";
                b.LastName1 = "WARD";
                b.LastName2 = "";
                b.MiddleName = "";
                b.IDType = "Passport";
                b.IDTypeID = 0;
                b.IDNumber = "86868686";
                b.IDSerialNumber = "";
                b.IDExpDate = new DateTime(2021, 08, 06);
                b.IDIssuedDate = new DateTime(2014, 08, 06);
                b.IDIssuedByCountry = "MX";
                b.IDIssuedByState = "CHIH";
                b.IDIssuedByStateID = 0;
                b.IDIssuedBy = "Goverment";
                b.IDIssuer = "";
                b.PhoneNumber = "526251140504";
                b.EmailAddress = "";
                b.Address = "123 Heinz Field";
                b.City = "CHIHUAHUA";
                b.CityID = -1;
                b.Country = "MEX";
                b.District = "";
                b.County = "";
                b.State = "CHIH";
                b.StateID = -1;
                b.PostalCode = "";
                b.DateOfBirth = new DateTime(1980, 08, 06);
                b.Nationality = "MX";
                b.CountryOfBirth = "1";
                b.BirthStateID = 0;
                b.BirthCityID = 0;
                b.BirthCity = "CHIHUAHUA";
                b.CountryOfResidence = "US";
                b.TaxID = "";
                b.Occupation = "";
                b.OccupationID = 0;
                b.Curp = "";
                b.TaxAmount = 0.00m;
                b.DoesNotHaveATaxID = true;
                b.CustRelationshipID = 0;
                b.CustRelationship = "";
                SenderInfo s = new SenderInfo();
                s.Address = "777 Heinz Field";
                s.BirthCity = "Miami";
                s.BirthCityID = 0;
                s.BirthStateID = 0;
                s.City = "Pittsburgh";
                s.CityID = 0;
                s.Country = "US";
                s.CountryOfBirth = "";
                s.CountryOfResidence = "";
                s.County = "";
                s.Curp = "";
                s.CustomerExternalID = "0";
                s.CustomerInternalID = 0;
                s.DateOfBirth = new DateTime(1980, 07, 07);
                s.District = "";
                s.DoesNotHaveATaxID = false;
                s.FirstName = "Ben";
                s.Gender = "M";
                s.IDExpDate = new DateTime(2018, 05, 05);
                s.IDIssuedBy = "RU";
                s.IDIssuedByCountry = "US";
                s.IDIssuedByState = "PA";
                s.IDIssuedByStateID = 0;
                s.IDIssuedDate = new DateTime(2010, 05, 05);
                s.IDNumber = "77770707";
                s.IDType = "Passport";
                s.IDTypeID = 205;
                s.LastName1 = "Roethlisberger";
                s.LastName2 = "";
                s.MiddleName = "";
                s.Name = "Ben Roethlisberger";
                s.Nationality = "US";
                s.Occupation = "Touchdown Creator";
                s.OccupationID = 0;
                s.PhoneNumber = "1-714 9996666";
                s.PostalCode = "";
                s.State = "PA";
                s.StateID = 0;
                s.TaxID = "2255887766";
                
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;
                
                request.TellerDrawerInstanceID = 2;
                request.OrderID = 0;
                request.OrderPIN = pin;
                request.OrderLookupCode = pin;
                request.RecAgentID = 11311;
                request.RecAgentLocID = 2679301;
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "Los Angeles";
                request.RecAgentState = "CA";
                request.RecAgentPostalCode = "";
                request.RecAgentCountry = "US";
                request.PayAgentID = 33260811;
                request.PayAgentLocID = 28901155;
                request.PayAgentBranch = "";
                request.PayAgentAddress = "123 Main";
                request.PayAgentCity = "Santa Ana";
                request.PayAgentState = "CA";
                request.PayAgentPostalCode = "";
                request.PayAgentCountry = "MX";
                request.PayAgentCountryID = 0;
                request.SendCurrency = "USD";
                request.SendAmount = 71.25m;
                request.SendCharge = 6.00m;
                request.PayoutCurrency = "EUR";
                request.PayoutAmount = 951.34m;
                request.PayAgentCountryID = 0;
                request.PayoutMethodID = 0;
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = 0;
                request.TransferReasonID = 0;
                request.TransferReason = "";
                request.ApproverID = 0;
                request.ConvertedCurrency = "";
                request.ConvertedRate = 14.58m;
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse response = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual("Order Paid", response.ReturnInfo.ErrorMessage);
                Assert.AreEqual(106, response.ReturnInfo.ErrorCode);

                Assert.AreEqual(false, response.ReturnInfo.AvailableForPayout);
                Assert.AreEqual(null, response.ConfirmationNumber);
                Assert.AreEqual(0.00m, response.BeneficiaryFee.Amount);
                Assert.AreEqual(null, response.BeneficiaryFee.CurrencyCode);
                Assert.AreEqual(persistenceID, response.PersistenceID);
                Assert.AreEqual(951.34m, response.BeneficiaryPayout.Amount);
                Assert.AreEqual("EUR", response.BeneficiaryPayout.CurrencyCode);
                Assert.AreEqual(false, response.IsValid);
                Assert.AreEqual(0, response.OrderID);
                Assert.AreEqual(null, response.OrderStatus);
                Assert.AreEqual(null, response.PayoutCommission);
                //Assert.AreEqual("", response.PayoutCommission.CurrencyCode);
                Assert.AreEqual(pin, response.PIN);
                //Assert.AreEqual(DateTime.UtcNow, response.ResponseDateTimeUTC);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

    }
}
