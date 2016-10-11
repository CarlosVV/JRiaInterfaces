using CES.CoreApi.Payout.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Payout.Controllers;
using System;
using CES.CoreApi.Payout.Service.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using AutoMapper;

using SimpleInjector;

namespace PayoutService.Test.WebAPITests
{

    [TestClass]
    class GetInfoAndPayout_Tests
    {
        private int m_goldencrownAgentID = 7884114;
        private readonly IRequestValidator _validator;
        private readonly IPayoutProcessor _processor;
        private readonly IMapper _mappingHelper;

        public GetInfoAndPayout_Tests()
        {
            //////Check incoming parameters:
            //if (validator == null) { throw new ArgumentNullException("validator"); }
            //if (processor == null) { throw new ArgumentNullException("processor"); }
            //if (mappingHelper == null) { throw new ArgumentNullException("mappingHelper"); }
            ////Assign params to instance vars.
            //_validator = validator;
            //_processor = processor;
            //_mappingHelper = mappingHelper;

            var container = new Container();
			CES.CoreApi.Payout.Api.DependenciesConfig .RegisterDependencies(container);
            _validator = container.GetInstance<IRequestValidator>();
            _processor = container.GetInstance<IPayoutProcessor>();
            _mappingHelper = container.GetInstance<IMapper>(); ;


        }

        [TestMethod]
        public void GetInfoAndPayout_GC_RUtoES()
        {
            string pin = "00136994234";
            int callingAgentID = 4123314;
            int callingAgentLocID = 843814;
            string locale = "es-cl";
            string callingCountry = "ES";
            string callingState = "M";
            int persistenceID = 0;

            //Required Fields for payout not returned in info call:
            string benCountry = "ES";
            string benIDIssuedCountry = "RU";
            string benIDType = "Passport";
            string benIDNumber = "1234567890";
            string benIDIssuedBy = "RU";
            string benNationality = "RU";
            DateTime benDOB = new DateTime(1980, 05, 15);
            DateTime benIDExpireDate = new DateTime(2020, 05, 15);
            DateTime benIDIssuedDate = new DateTime(2006, 05, 15);
            string benPostalCode = "12345";
            int benCityID = -1;
            int benStateID = -1;
            int payMethodID = 1;

            string recAgentState = "ES";

            //Request Header:
            RequesterInfo ri = new RequesterInfo();
            ri.AppID = 500;
            ri.AppObjectID = 100;
            ri.AgentID = callingAgentID;
            ri.AgentLocID = callingAgentLocID;
            ri.UserID = 1;
            ri.UserLoginID = 1;
            ri.TerminalID = "0";
            ri.TerminalUserID = "0";
            ri.LocalTime = DateTime.Now;
            ri.UtcTime = DateTime.UtcNow;
            ri.Timezone = "-7";
            ri.TimezoneID = 8701;
            ri.Locale = locale;
            ri.Version = "1.0";
            ri.AgentCountry = callingCountry;
            ri.AgentState = callingState;

            //GET TRANS INFO:
            GetTransactionInfoResponse gtiResp = null;
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = callingCountry;
                request.StateTo = callingState;
                //Make the Call:
                //gtiResp = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
               var controller = new PayoutController(_validator,_processor,_mappingHelper);

                gtiResp=controller.GetTransactionInfo(request) as GetTransactionInfoResponse;
                //Check the Response:
                Assert.AreEqual(null, gtiResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(0, gtiResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, gtiResp.ReturnInfo.AvailableForPayout);
                //Checks a subset of all the fields.
                Assert.IsTrue(gtiResp.PersistenceID > 0);
                persistenceID = gtiResp.PersistenceID;//Set persistence ID to use in next call.
                Assert.AreEqual(new DateTime(2016, 7, 12, 0, 0, 0), gtiResp.TransferDate);
                Assert.AreEqual("Ready for Payout", gtiResp.TransferStatus);
                Assert.AreEqual(117.00m, gtiResp.PayoutAmount.Amount);
                Assert.AreEqual("EUR", gtiResp.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", gtiResp.CountryFrom);
                Assert.AreEqual(pin, gtiResp.OrderPIN);
                Assert.AreEqual("Cash Pickup", gtiResp.DeliveryMethod);
                Assert.AreEqual(5.85m, gtiResp.BeneficiaryTax);
                Assert.AreEqual(111.15m, gtiResp.NetAmount);
                Assert.AreEqual("NOLIIK  ADELA ", gtiResp.SenderInfo.Name);
                Assert.AreEqual("NOLIIK", gtiResp.SenderInfo.FirstName);
                Assert.AreEqual("ADELA", gtiResp.SenderInfo.LastName1);
                Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", gtiResp.SenderInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", gtiResp.SenderInfo.City);
                Assert.AreEqual("N/A", gtiResp.SenderInfo.State);
                Assert.AreEqual("RU", gtiResp.SenderInfo.Country);
                Assert.AreEqual("889250989", gtiResp.SenderInfo.PhoneNumber);
                Assert.AreEqual("INES  MARIA ", gtiResp.BeneficiaryInfo.Name);
                Assert.AreEqual("INES", gtiResp.BeneficiaryInfo.FirstName);
                Assert.AreEqual("MARIA", gtiResp.BeneficiaryInfo.LastName1);
                Assert.AreEqual("+79139940074", gtiResp.BeneficiaryInfo.PhoneNumber);
                //Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.BeneficiaryInfo.Address);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.Address);
                //Assert.AreEqual("NOVOSIBIRSK", response.BeneficiaryInfo.City);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.City);
                Assert.AreEqual("N/A", gtiResp.BeneficiaryInfo.State);
                Assert.AreEqual(5000, gtiResp.ProviderInfo.ProviderID);
                Assert.AreEqual(1, gtiResp.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Golden Crown", gtiResp.ProviderInfo.ProviderName);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.BenExternalID);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }

            //PAYOUT TRANS:
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
                request.PersistenceID = persistenceID;
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = gtiResp.BeneficiaryInfo.SourceOfFunds;
                b.Gender = gtiResp.BeneficiaryInfo.Gender;
                b.TaxCountry = gtiResp.BeneficiaryInfo.TaxCountry;
                b.Name = gtiResp.BeneficiaryInfo.Name;
                b.FirstName = gtiResp.BeneficiaryInfo.FirstName;
                b.LastName1 = gtiResp.BeneficiaryInfo.LastName1;
                b.LastName2 = gtiResp.BeneficiaryInfo.LastName2;
                b.MiddleName = gtiResp.BeneficiaryInfo.MiddleName;
                b.IDType = benIDType;//TODO: UNIT TEST:  gtiResp.BeneficiaryInfo.IDType;
                b.IDTypeID = gtiResp.BeneficiaryInfo.IDTypeID;
                b.IDNumber = benIDNumber;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDNumber;
                b.IDSerialNumber = gtiResp.BeneficiaryInfo.IDSerialNumber;
                b.IDExpDate = benIDExpireDate;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDExpDate;
                b.IDIssuedDate = benIDIssuedDate;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedDate;
                b.IDIssuedByCountry = benIDIssuedCountry;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedByCountry;
                b.IDIssuedByState = gtiResp.BeneficiaryInfo.IDIssuedByState;
                b.IDIssuedByStateID = gtiResp.BeneficiaryInfo.IDIssuedByStateID;
                b.IDIssuedBy = benIDIssuedBy;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedBy;
                b.IDIssuer = gtiResp.BeneficiaryInfo.IDIssuer;
                b.PhoneNumber = gtiResp.BeneficiaryInfo.PhoneNumber;
                b.EmailAddress = gtiResp.BeneficiaryInfo.EmailAddress;
                b.Address = gtiResp.BeneficiaryInfo.Address;
                b.City = gtiResp.BeneficiaryInfo.City;
                b.CityID = benCityID;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.CityID;
                b.Country = benCountry;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.Country;
                b.District = gtiResp.BeneficiaryInfo.District;
                b.County = gtiResp.BeneficiaryInfo.County;
                b.State = gtiResp.BeneficiaryInfo.State;
                b.StateID = benStateID;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.StateID;
                b.PostalCode = benPostalCode;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.PostalCode;
                b.DateOfBirth = benDOB;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.DateOfBirth;
                b.Nationality = benNationality;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.Nationality;
                b.CountryOfBirth = gtiResp.BeneficiaryInfo.CountryOfBirth;
                b.BirthStateID = gtiResp.BeneficiaryInfo.BirthStateID;
                b.BirthCityID = gtiResp.BeneficiaryInfo.BirthCityID;
                b.BirthCity = gtiResp.BeneficiaryInfo.BirthCity;
                b.CountryOfResidence = gtiResp.BeneficiaryInfo.CountryOfResidence;
                b.TaxID = gtiResp.BeneficiaryInfo.TaxID;
                b.Occupation = gtiResp.BeneficiaryInfo.Occupation;
                b.OccupationID = gtiResp.BeneficiaryInfo.OccupationID;
                b.Curp = gtiResp.BeneficiaryInfo.Curp;
                b.TaxAmount = gtiResp.BeneficiaryInfo.TaxAmount;
                b.DoesNotHaveATaxID = gtiResp.BeneficiaryInfo.DoesNotHaveATaxID;
                b.CustRelationshipID = gtiResp.BeneficiaryInfo.CustRelationshipID;
                b.CustRelationship = gtiResp.BeneficiaryInfo.CustRelationship;
                SenderInfo s = new SenderInfo();
                s.Address = gtiResp.SenderInfo.Address;
                s.BirthCity = gtiResp.SenderInfo.BirthCity;
                s.BirthCityID = gtiResp.SenderInfo.BirthCityID;
                s.BirthStateID = gtiResp.SenderInfo.BirthStateID;
                s.City = gtiResp.SenderInfo.City;
                s.CityID = gtiResp.SenderInfo.CityID;
                s.Country = gtiResp.SenderInfo.Country;
                s.CountryOfBirth = gtiResp.SenderInfo.CountryOfBirth;
                s.CountryOfResidence = gtiResp.SenderInfo.CountryOfResidence;
                s.County = gtiResp.SenderInfo.County;
                s.Curp = gtiResp.SenderInfo.Curp;
                s.CustomerExternalID = gtiResp.SenderInfo.CustomerExternalID;
                s.CustomerInternalID = gtiResp.SenderInfo.CustomerInternalID;
                s.DateOfBirth = gtiResp.SenderInfo.DateOfBirth;
                s.District = gtiResp.SenderInfo.District;
                s.DoesNotHaveATaxID = gtiResp.SenderInfo.DoesNotHaveATaxID;
                s.FirstName = gtiResp.SenderInfo.FirstName;
                s.Gender = gtiResp.SenderInfo.Gender;
                s.IDExpDate = gtiResp.SenderInfo.IDExpDate;
                s.IDIssuedBy = gtiResp.SenderInfo.IDIssuedBy;
                s.IDIssuedByCountry = gtiResp.SenderInfo.IDIssuedByCountry;
                s.IDIssuedByState = gtiResp.SenderInfo.IDIssuedByState;
                s.IDIssuedByStateID = gtiResp.SenderInfo.IDIssuedByStateID;
                s.IDIssuedDate = gtiResp.SenderInfo.IDIssuedDate;
                s.IDNumber = gtiResp.SenderInfo.IDNumber;
                s.IDType = gtiResp.SenderInfo.IDType;
                s.IDTypeID = gtiResp.SenderInfo.IDTypeID;
                s.LastName1 = gtiResp.SenderInfo.LastName1;
                s.LastName2 = gtiResp.SenderInfo.LastName2;
                s.MiddleName = gtiResp.SenderInfo.MiddleName;
                s.Name = gtiResp.SenderInfo.Name;
                s.Nationality = gtiResp.SenderInfo.Nationality;
                s.Occupation = gtiResp.SenderInfo.Occupation;
                s.OccupationID = gtiResp.SenderInfo.OccupationID;
                s.PhoneNumber = gtiResp.SenderInfo.PhoneNumber;
                s.PostalCode = gtiResp.SenderInfo.PostalCode;
                s.State = gtiResp.SenderInfo.State;
                s.StateID = gtiResp.SenderInfo.StateID;
                s.TaxID = gtiResp.SenderInfo.TaxID;
              

                //Set the main objects to the request.
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;              

                request.TellerDrawerInstanceID = 17;
                request.OrderID = 0;
                request.OrderPIN = pin;
                request.OrderLookupCode = pin;
                request.RecAgentID = m_goldencrownAgentID;
                request.RecAgentLocID = Convert.ToInt32(gtiResp.RecAgentID);
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "";
                request.RecAgentState = recAgentState;
                request.RecAgentPostalCode = "";
                request.RecAgentCountry = gtiResp.CountryFrom;
                request.PayAgentID = callingAgentID;
                request.PayAgentLocID = callingAgentLocID;
                request.PayAgentBranch = gtiResp.PayAgentBranchName;
                request.PayAgentAddress = "";
                request.PayAgentCity = "";
                request.PayAgentState = callingState;
                request.PayAgentPostalCode = "";
                request.PayAgentCountry = callingCountry;
                request.PayAgentCountryID = -1;
                request.SendCurrency = gtiResp.PayoutAmount.CurrencyCode;//TODO: UNIT TEST:  gtiResp.SendAmount.CurrencyCode;
                request.SendAmount = gtiResp.PayoutAmount.Amount;//TODO: UNIT TEST:  gtiResp.SendAmount.Amount;
                request.SendCharge = 0.00m;//TODO: UNIT TEST:  gtiResp.SenderComission.Amount;
                request.PayoutCurrency = gtiResp.PayoutAmount.CurrencyCode;
                request.PayoutAmount = gtiResp.PayoutAmount.Amount;
                request.PayAgentCountryID = -1;
                request.PayoutMethodID = payMethodID;//TODO: UNIT TEST:  Convert.ToInt32(gtiResp.DeliveryMethod);
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = -1;
                request.TransferReasonID = -1;
                request.TransferReason = "";
                request.ApproverID = -1;
                request.ConvertedCurrency = "";
                request.ConvertedRate = Convert.ToDecimal(gtiResp.ExchangeRate);
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse ptResp = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual(null, ptResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(1, ptResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, ptResp.ReturnInfo.AvailableForPayout);
                Assert.AreEqual(pin, ptResp.ConfirmationNumber);
                Assert.AreEqual(0.59m, ptResp.BeneficiaryFee.Amount);
                Assert.AreEqual("EUR", ptResp.BeneficiaryFee.CurrencyCode);
                Assert.AreEqual(persistenceID, ptResp.PersistenceID);
                Assert.AreEqual("", ptResp.BeneficiaryPayout.Amount);
                Assert.AreEqual("EUR", ptResp.BeneficiaryPayout.CurrencyCode);
                Assert.AreEqual(true, ptResp.IsValid);
                Assert.AreEqual(0, ptResp.OrderID);
                Assert.AreEqual("", ptResp.OrderStatus);
                Assert.AreEqual("", ptResp.PayoutCommission.Amount);
                Assert.AreEqual("", ptResp.PayoutCommission.CurrencyCode);
                Assert.AreEqual(pin, ptResp.PIN);
                Assert.AreEqual(DateTime.UtcNow, ptResp.ResponseDateTimeUTC);
                if (ptResp.PayoutRequiredFields != null)
                {
                    foreach (PayoutFields f in ptResp.PayoutRequiredFields)
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
        public void GetInfoAndPayout_GC_RUtoES_AlreadyPaid()
        {
            string pin = "00620670915";
            int callingAgentID = 44729311;
            int callingAgentLocID = 59347511;
            string locale = "es-cl";
            string callingCountry = "ES";
            string callingState = "RM";
            int persistenceID = 0;

            //Required Fields for payout not returned in info call:
            string benCountry = "ES";
            string benIDIssuedCountry = "RU";
            string benIDType = "Passport";
            string benIDNumber = "1234567890";
            string benIDIssuedBy = "RU";
            string benNationality = "RU";
            DateTime benDOB = new DateTime(1980, 05, 15);
            DateTime benIDExpireDate = new DateTime(2020, 05, 15);
            DateTime benIDIssuedDate = new DateTime(2006, 05, 15);
            string benPostalCode = "12345";
            int benCityID = -1;
            int benStateID = -1;
            int payMethodID = 1;

            string recAgentState = "ES";

            //Request Header:
            RequesterInfo ri = new RequesterInfo();
            ri.AppID = 501;
            ri.AppObjectID = 100;
            ri.AgentID = callingAgentID;
            ri.AgentLocID = callingAgentLocID;
            ri.UserID = 80912;
            ri.UserLoginID = 80912;
            ri.TerminalID = "0";
            ri.TerminalUserID = "0";
            ri.LocalTime = DateTime.Now;
            ri.UtcTime = DateTime.UtcNow;
            ri.Timezone = "-3";
            ri.TimezoneID = 8701;
            ri.Locale = locale;
            ri.Version = "1.0";
            ri.AgentCountry = callingCountry;
            ri.AgentState = callingState;

            //GET TRANS INFO:
            GetTransactionInfoResponse gtiResp = null;
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = callingCountry;
                request.StateTo = callingState;
                //Make the Call:
                gtiResp = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual("Paid Out", gtiResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(7, gtiResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, gtiResp.ReturnInfo.AvailableForPayout);
                //Checks a subset of all the fields.
                Assert.IsTrue(gtiResp.PersistenceID > 0);
                persistenceID = gtiResp.PersistenceID;//Set persistence ID to use in next call.
                Assert.AreEqual(new DateTime(2016, 7, 12, 0, 0, 0), gtiResp.TransferDate);
                Assert.AreEqual("Paid Out", gtiResp.TransferStatus);
                Assert.AreEqual(117.00m, gtiResp.PayoutAmount.Amount);
                Assert.AreEqual("EUR", gtiResp.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", gtiResp.CountryFrom);
                Assert.AreEqual(pin, gtiResp.OrderPIN);
                Assert.AreEqual("Cash Pickup", gtiResp.DeliveryMethod);
                Assert.AreEqual(5.85m, gtiResp.BeneficiaryTax);
                Assert.AreEqual(111.15m, gtiResp.NetAmount);
                Assert.AreEqual("NOLIEK  ADELA ", gtiResp.SenderInfo.Name);
                Assert.AreEqual("NOLIEK", gtiResp.SenderInfo.FirstName);
                Assert.AreEqual("ADELA", gtiResp.SenderInfo.LastName1);
                Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", gtiResp.SenderInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", gtiResp.SenderInfo.City);
                Assert.AreEqual("N/A", gtiResp.SenderInfo.State);
                Assert.AreEqual("RU", gtiResp.SenderInfo.Country);
                Assert.AreEqual("889250989", gtiResp.SenderInfo.PhoneNumber);
                Assert.AreEqual("INES  MARIA ", gtiResp.BeneficiaryInfo.Name);
                Assert.AreEqual("INES", gtiResp.BeneficiaryInfo.FirstName);
                Assert.AreEqual("MARIA", gtiResp.BeneficiaryInfo.LastName1);
                Assert.AreEqual("+79139940074", gtiResp.BeneficiaryInfo.PhoneNumber);
                //Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.BeneficiaryInfo.Address);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.Address);
                //Assert.AreEqual("NOVOSIBIRSK", response.BeneficiaryInfo.City);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.City);
                Assert.AreEqual("N/A", gtiResp.BeneficiaryInfo.State);
                Assert.AreEqual(5000, gtiResp.ProviderInfo.ProviderID);
                Assert.AreEqual(1, gtiResp.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Golden Crown", gtiResp.ProviderInfo.ProviderName);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.BenExternalID);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }

            //PAYOUT TRANS:
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
                request.PersistenceID = persistenceID;
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = gtiResp.BeneficiaryInfo.SourceOfFunds;
                b.Gender = gtiResp.BeneficiaryInfo.Gender;
                b.TaxCountry = gtiResp.BeneficiaryInfo.TaxCountry;
                b.Name = gtiResp.BeneficiaryInfo.Name;
                b.FirstName = gtiResp.BeneficiaryInfo.FirstName;
                b.LastName1 = gtiResp.BeneficiaryInfo.LastName1;
                b.LastName2 = gtiResp.BeneficiaryInfo.LastName2;
                b.MiddleName = gtiResp.BeneficiaryInfo.MiddleName;
                b.IDType = benIDType;//TODO: UNIT TEST:  gtiResp.BeneficiaryInfo.IDType;
                b.IDTypeID = gtiResp.BeneficiaryInfo.IDTypeID;
                b.IDNumber = benIDNumber;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDNumber;
                b.IDSerialNumber = gtiResp.BeneficiaryInfo.IDSerialNumber;
                b.IDExpDate = benIDExpireDate;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDExpDate;
                b.IDIssuedDate = benIDIssuedDate;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedDate;
                b.IDIssuedByCountry = benIDIssuedCountry;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedByCountry;
                b.IDIssuedByState = gtiResp.BeneficiaryInfo.IDIssuedByState;
                b.IDIssuedByStateID = gtiResp.BeneficiaryInfo.IDIssuedByStateID;
                b.IDIssuedBy = benIDIssuedBy;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedBy;
                b.IDIssuer = gtiResp.BeneficiaryInfo.IDIssuer;
                b.PhoneNumber = gtiResp.BeneficiaryInfo.PhoneNumber;
                b.EmailAddress = gtiResp.BeneficiaryInfo.EmailAddress;
                b.Address = gtiResp.BeneficiaryInfo.Address;
                b.City = gtiResp.BeneficiaryInfo.City;
                b.CityID = benCityID;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.CityID;
                b.Country = benCountry;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.Country;
                b.District = gtiResp.BeneficiaryInfo.District;
                b.County = gtiResp.BeneficiaryInfo.County;
                b.State = gtiResp.BeneficiaryInfo.State;
                b.StateID = benStateID;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.StateID;
                b.PostalCode = benPostalCode;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.PostalCode;
                b.DateOfBirth = benDOB;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.DateOfBirth;
                b.Nationality = benNationality;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.Nationality;
                b.CountryOfBirth = gtiResp.BeneficiaryInfo.CountryOfBirth;
                b.BirthStateID = gtiResp.BeneficiaryInfo.BirthStateID;
                b.BirthCityID = gtiResp.BeneficiaryInfo.BirthCityID;
                b.BirthCity = gtiResp.BeneficiaryInfo.BirthCity;
                b.CountryOfResidence = gtiResp.BeneficiaryInfo.CountryOfResidence;
                b.TaxID = gtiResp.BeneficiaryInfo.TaxID;
                b.Occupation = gtiResp.BeneficiaryInfo.Occupation;
                b.OccupationID = gtiResp.BeneficiaryInfo.OccupationID;
                b.Curp = gtiResp.BeneficiaryInfo.Curp;
                b.TaxAmount = gtiResp.BeneficiaryInfo.TaxAmount;
                b.DoesNotHaveATaxID = gtiResp.BeneficiaryInfo.DoesNotHaveATaxID;
                b.CustRelationshipID = gtiResp.BeneficiaryInfo.CustRelationshipID;
                b.CustRelationship = gtiResp.BeneficiaryInfo.CustRelationship;
                SenderInfo s = new SenderInfo();
                s.Address = gtiResp.SenderInfo.Address;
                s.BirthCity = gtiResp.SenderInfo.BirthCity;
                s.BirthCityID = gtiResp.SenderInfo.BirthCityID;
                s.BirthStateID = gtiResp.SenderInfo.BirthStateID;
                s.City = gtiResp.SenderInfo.City;
                s.CityID = gtiResp.SenderInfo.CityID;
                s.Country = gtiResp.SenderInfo.Country;
                s.CountryOfBirth = gtiResp.SenderInfo.CountryOfBirth;
                s.CountryOfResidence = gtiResp.SenderInfo.CountryOfResidence;
                s.County = gtiResp.SenderInfo.County;
                s.Curp = gtiResp.SenderInfo.Curp;
                s.CustomerExternalID = gtiResp.SenderInfo.CustomerExternalID;
                s.CustomerInternalID = gtiResp.SenderInfo.CustomerInternalID;
                s.DateOfBirth = gtiResp.SenderInfo.DateOfBirth;
                s.District = gtiResp.SenderInfo.District;
                s.DoesNotHaveATaxID = gtiResp.SenderInfo.DoesNotHaveATaxID;
                s.FirstName = gtiResp.SenderInfo.FirstName;
                s.Gender = gtiResp.SenderInfo.Gender;
                s.IDExpDate = gtiResp.SenderInfo.IDExpDate;
                s.IDIssuedBy = gtiResp.SenderInfo.IDIssuedBy;
                s.IDIssuedByCountry = gtiResp.SenderInfo.IDIssuedByCountry;
                s.IDIssuedByState = gtiResp.SenderInfo.IDIssuedByState;
                s.IDIssuedByStateID = gtiResp.SenderInfo.IDIssuedByStateID;
                s.IDIssuedDate = gtiResp.SenderInfo.IDIssuedDate;
                s.IDNumber = gtiResp.SenderInfo.IDNumber;
                s.IDType = gtiResp.SenderInfo.IDType;
                s.IDTypeID = gtiResp.SenderInfo.IDTypeID;
                s.LastName1 = gtiResp.SenderInfo.LastName1;
                s.LastName2 = gtiResp.SenderInfo.LastName2;
                s.MiddleName = gtiResp.SenderInfo.MiddleName;
                s.Name = gtiResp.SenderInfo.Name;
                s.Nationality = gtiResp.SenderInfo.Nationality;
                s.Occupation = gtiResp.SenderInfo.Occupation;
                s.OccupationID = gtiResp.SenderInfo.OccupationID;
                s.PhoneNumber = gtiResp.SenderInfo.PhoneNumber;
                s.PostalCode = gtiResp.SenderInfo.PostalCode;
                s.State = gtiResp.SenderInfo.State;
                s.StateID = gtiResp.SenderInfo.StateID;
                s.TaxID = gtiResp.SenderInfo.TaxID;
                
                //Set the main objects to the request.
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;
               
                request.TellerDrawerInstanceID = 17;
                request.OrderID = 0;
                request.OrderPIN = pin;
                request.OrderLookupCode = pin;
                request.RecAgentID = m_goldencrownAgentID;
                request.RecAgentLocID = Convert.ToInt32(gtiResp.RecAgentID);
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "";
                request.RecAgentState = recAgentState;
                request.RecAgentPostalCode = "";
                request.RecAgentCountry = gtiResp.CountryFrom;
                request.PayAgentID = callingAgentID;
                request.PayAgentLocID = callingAgentLocID;
                request.PayAgentBranch = gtiResp.PayAgentBranchName;
                request.PayAgentAddress = "";
                request.PayAgentCity = "";
                request.PayAgentState = callingState;
                request.PayAgentPostalCode = "";
                request.PayAgentCountry = callingCountry;
                request.PayAgentCountryID = -1;
                request.SendCurrency = gtiResp.PayoutAmount.CurrencyCode;//TODO: UNIT TEST:  gtiResp.SendAmount.CurrencyCode;
                request.SendAmount = gtiResp.PayoutAmount.Amount;//TODO: UNIT TEST:  gtiResp.SendAmount.Amount;
                request.SendCharge = 0.00m;//TODO: UNIT TEST:  gtiResp.SenderComission.Amount;
                request.PayoutCurrency = gtiResp.PayoutAmount.CurrencyCode;
                request.PayoutAmount = gtiResp.PayoutAmount.Amount;
                request.PayAgentCountryID = -1;
                request.PayoutMethodID = payMethodID;//TODO: UNIT TEST:  Convert.ToInt32(gtiResp.DeliveryMethod);
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = -1;
                request.TransferReasonID = -1;
                request.TransferReason = "";
                request.ApproverID = -1;
                request.ConvertedCurrency = "";
                request.ConvertedRate = Convert.ToDecimal(gtiResp.ExchangeRate);
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse ptResp = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual("Payout Service: Order not available", ptResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(9010, ptResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, ptResp.ReturnInfo.AvailableForPayout);
                Assert.AreEqual(null, ptResp.ConfirmationNumber);
                Assert.AreEqual(null, ptResp.BeneficiaryFee);
                Assert.AreEqual(0, ptResp.PersistenceID);
                Assert.AreEqual("", ptResp.BeneficiaryPayout.Amount);
                Assert.AreEqual("EUR", ptResp.BeneficiaryPayout.CurrencyCode);
                Assert.AreEqual(true, ptResp.IsValid);
                Assert.AreEqual(0, ptResp.OrderID);
                Assert.AreEqual("", ptResp.OrderStatus);
                Assert.AreEqual("", ptResp.PayoutCommission.Amount);
                Assert.AreEqual("", ptResp.PayoutCommission.CurrencyCode);
                Assert.AreEqual(pin, ptResp.PIN);
                Assert.AreEqual(DateTime.UtcNow, ptResp.ResponseDateTimeUTC);
                if (ptResp.PayoutRequiredFields != null)
                {
                    foreach (PayoutFields f in ptResp.PayoutRequiredFields)
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
        public void GetInfoAndPayout_GC_RUtoUS()
        {
            string pin = "00297829508";
            int callingAgentID = 39314611;
            int callingAgentLocID = 52651211;
            string locale = "en-us";
            string callingCountry = "US";
            string callingState = "AZ";
            int persistenceID = 0;

            //Required Fields for payout not returned in info call:
            string benCountry = "US";
            string benIDIssuedCountry = "RU";
            string benIDType = "Passport";
            string benIDNumber = "1111155555";
            string benIDIssuedBy = "RU";
            string benNationality = "RU";
            DateTime benDOB = new DateTime(1980, 06, 16);
            DateTime benIDExpireDate = new DateTime(2020, 06, 16);
            DateTime benIDIssuedDate = new DateTime(2006, 06, 16);
            string benPostalCode = "11111";
            int benCityID = -1;
            int benStateID = -1;
            int payMethodID = 1;

            string recAgentState = "AZ";

            //Request Header:
            RequesterInfo ri = new RequesterInfo();
            ri.AppID = 501;
            ri.AppObjectID = 100;
            ri.AgentID = callingAgentID;
            ri.AgentLocID = callingAgentLocID;
            ri.UserID = 80912;
            ri.UserLoginID = 80912;
            ri.TerminalID = "0";
            ri.TerminalUserID = "0";
            ri.LocalTime = DateTime.Now;
            ri.UtcTime = DateTime.UtcNow;
            ri.Timezone = "-3";
            ri.TimezoneID = 8701;
            ri.Locale = locale;
            ri.Version = "1.0";
            ri.AgentCountry = callingCountry;
            ri.AgentState = callingState;

            //GET TRANS INFO:
            GetTransactionInfoResponse gtiResp = null;
            try
            {
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = callingCountry;
                request.StateTo = callingState;
                //Make the Call:
                gtiResp = PayoutWebAPI_Client.GetTransactionInfoRequest(request);
                //Check the Response:
                Assert.AreEqual(null, gtiResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(0, gtiResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, gtiResp.ReturnInfo.AvailableForPayout);
                //Checks a subset of all the fields.
                Assert.IsTrue(gtiResp.PersistenceID > 0);
                persistenceID = gtiResp.PersistenceID;//Set persistence ID to use in next call.
                Assert.AreEqual(new DateTime(2016, 7, 12, 0, 0, 0), gtiResp.TransferDate);
                Assert.AreEqual("Ready for Payout", gtiResp.TransferStatus);
                Assert.AreEqual(109.00m, gtiResp.PayoutAmount.Amount);
                Assert.AreEqual("USD", gtiResp.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", gtiResp.CountryFrom);
                Assert.AreEqual(pin, gtiResp.OrderPIN);
                Assert.AreEqual("Cash Pickup", gtiResp.DeliveryMethod);
                Assert.AreEqual(5.45m, gtiResp.BeneficiaryTax);
                Assert.AreEqual(103.55m, gtiResp.NetAmount);
                Assert.AreEqual("NOLAY  ADELA ", gtiResp.SenderInfo.Name);
                Assert.AreEqual("NOLAY", gtiResp.SenderInfo.FirstName);
                Assert.AreEqual("ADELA", gtiResp.SenderInfo.LastName1);
                Assert.AreEqual("RONDA DE PONIENTE, 11 - 2D", gtiResp.SenderInfo.Address);
                Assert.AreEqual("NOVOSIBIRSK", gtiResp.SenderInfo.City);
                Assert.AreEqual("N/A", gtiResp.SenderInfo.State);
                Assert.AreEqual("RU", gtiResp.SenderInfo.Country);
                Assert.AreEqual("889250989", gtiResp.SenderInfo.PhoneNumber);
                Assert.AreEqual("INES  MARIA ", gtiResp.BeneficiaryInfo.Name);
                Assert.AreEqual("INES", gtiResp.BeneficiaryInfo.FirstName);
                Assert.AreEqual("MARIA", gtiResp.BeneficiaryInfo.LastName1);
                Assert.AreEqual("+79139940074", gtiResp.BeneficiaryInfo.PhoneNumber);
                //Assert.AreEqual("C\\ RONDA DE PONIENTE, 11 - 2D", response.BeneficiaryInfo.Address);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.Address);
                //Assert.AreEqual("NOVOSIBIRSK", response.BeneficiaryInfo.City);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.City);
                Assert.AreEqual("N/A", gtiResp.BeneficiaryInfo.State);
                Assert.AreEqual(5000, gtiResp.ProviderInfo.ProviderID);
                Assert.AreEqual(1, gtiResp.ProviderInfo.ProviderTypeID);
                Assert.AreEqual("Golden Crown", gtiResp.ProviderInfo.ProviderName);
                Assert.AreEqual(null, gtiResp.BeneficiaryInfo.BenExternalID);
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }

            //PAYOUT TRANS:
            try
            {
                //Format request:
                PayoutTransactionRequest request = new PayoutTransactionRequest();
                request.PersistenceID = persistenceID;
                BeneficiaryInfo b = new BeneficiaryInfo();
                b.SourceOfFunds = gtiResp.BeneficiaryInfo.SourceOfFunds;
                b.Gender = gtiResp.BeneficiaryInfo.Gender;
                b.TaxCountry = gtiResp.BeneficiaryInfo.TaxCountry;
                b.Name = gtiResp.BeneficiaryInfo.Name;
                b.FirstName = gtiResp.BeneficiaryInfo.FirstName;
                b.LastName1 = gtiResp.BeneficiaryInfo.LastName1;
                b.LastName2 = gtiResp.BeneficiaryInfo.LastName2;
                b.MiddleName = gtiResp.BeneficiaryInfo.MiddleName;
                b.IDType = benIDType;//TODO: UNIT TEST:  gtiResp.BeneficiaryInfo.IDType;
                b.IDTypeID = gtiResp.BeneficiaryInfo.IDTypeID;
                b.IDNumber = benIDNumber;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDNumber;
                b.IDSerialNumber = gtiResp.BeneficiaryInfo.IDSerialNumber;
                b.IDExpDate = benIDExpireDate;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDExpDate;
                b.IDIssuedDate = benIDIssuedDate;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedDate;
                b.IDIssuedByCountry = benIDIssuedCountry;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedByCountry;
                b.IDIssuedByState = gtiResp.BeneficiaryInfo.IDIssuedByState;
                b.IDIssuedByStateID = gtiResp.BeneficiaryInfo.IDIssuedByStateID;
                b.IDIssuedBy = benIDIssuedBy;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.IDIssuedBy;
                b.IDIssuer = gtiResp.BeneficiaryInfo.IDIssuer;
                b.PhoneNumber = gtiResp.BeneficiaryInfo.PhoneNumber;
                b.EmailAddress = gtiResp.BeneficiaryInfo.EmailAddress;
                b.Address = gtiResp.BeneficiaryInfo.Address;
                b.City = gtiResp.BeneficiaryInfo.City;
                b.CityID = benCityID;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.CityID;
                b.Country = benCountry;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.Country;
                b.District = gtiResp.BeneficiaryInfo.District;
                b.County = gtiResp.BeneficiaryInfo.County;
                b.State = gtiResp.BeneficiaryInfo.State;
                b.StateID = benStateID;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.StateID;
                b.PostalCode = benPostalCode;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.PostalCode;
                b.DateOfBirth = benDOB;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.DateOfBirth;
                b.Nationality = benNationality;//TODO: UNIT TEST: gtiResp.BeneficiaryInfo.Nationality;
                b.CountryOfBirth = gtiResp.BeneficiaryInfo.CountryOfBirth;
                b.BirthStateID = gtiResp.BeneficiaryInfo.BirthStateID;
                b.BirthCityID = gtiResp.BeneficiaryInfo.BirthCityID;
                b.BirthCity = gtiResp.BeneficiaryInfo.BirthCity;
                b.CountryOfResidence = gtiResp.BeneficiaryInfo.CountryOfResidence;
                b.TaxID = gtiResp.BeneficiaryInfo.TaxID;
                b.Occupation = gtiResp.BeneficiaryInfo.Occupation;
                b.OccupationID = gtiResp.BeneficiaryInfo.OccupationID;
                b.Curp = gtiResp.BeneficiaryInfo.Curp;
                b.TaxAmount = gtiResp.BeneficiaryInfo.TaxAmount;
                b.DoesNotHaveATaxID = gtiResp.BeneficiaryInfo.DoesNotHaveATaxID;
                b.CustRelationshipID = gtiResp.BeneficiaryInfo.CustRelationshipID;
                b.CustRelationship = gtiResp.BeneficiaryInfo.CustRelationship;
                SenderInfo s = new SenderInfo();
                s.Address = gtiResp.SenderInfo.Address;
                s.BirthCity = gtiResp.SenderInfo.BirthCity;
                s.BirthCityID = gtiResp.SenderInfo.BirthCityID;
                s.BirthStateID = gtiResp.SenderInfo.BirthStateID;
                s.City = gtiResp.SenderInfo.City;
                s.CityID = gtiResp.SenderInfo.CityID;
                s.Country = gtiResp.SenderInfo.Country;
                s.CountryOfBirth = gtiResp.SenderInfo.CountryOfBirth;
                s.CountryOfResidence = gtiResp.SenderInfo.CountryOfResidence;
                s.County = gtiResp.SenderInfo.County;
                s.Curp = gtiResp.SenderInfo.Curp;
                s.CustomerExternalID = gtiResp.SenderInfo.CustomerExternalID;
                s.CustomerInternalID = gtiResp.SenderInfo.CustomerInternalID;
                s.DateOfBirth = gtiResp.SenderInfo.DateOfBirth;
                s.District = gtiResp.SenderInfo.District;
                s.DoesNotHaveATaxID = gtiResp.SenderInfo.DoesNotHaveATaxID;
                s.FirstName = gtiResp.SenderInfo.FirstName;
                s.Gender = gtiResp.SenderInfo.Gender;
                s.IDExpDate = gtiResp.SenderInfo.IDExpDate;
                s.IDIssuedBy = gtiResp.SenderInfo.IDIssuedBy;
                s.IDIssuedByCountry = gtiResp.SenderInfo.IDIssuedByCountry;
                s.IDIssuedByState = gtiResp.SenderInfo.IDIssuedByState;
                s.IDIssuedByStateID = gtiResp.SenderInfo.IDIssuedByStateID;
                s.IDIssuedDate = gtiResp.SenderInfo.IDIssuedDate;
                s.IDNumber = gtiResp.SenderInfo.IDNumber;
                s.IDType = gtiResp.SenderInfo.IDType;
                s.IDTypeID = gtiResp.SenderInfo.IDTypeID;
                s.LastName1 = gtiResp.SenderInfo.LastName1;
                s.LastName2 = gtiResp.SenderInfo.LastName2;
                s.MiddleName = gtiResp.SenderInfo.MiddleName;
                s.Name = gtiResp.SenderInfo.Name;
                s.Nationality = gtiResp.SenderInfo.Nationality;
                s.Occupation = gtiResp.SenderInfo.Occupation;
                s.OccupationID = gtiResp.SenderInfo.OccupationID;
                s.PhoneNumber = gtiResp.SenderInfo.PhoneNumber;
                s.PostalCode = gtiResp.SenderInfo.PostalCode;
                s.State = gtiResp.SenderInfo.State;
                s.StateID = gtiResp.SenderInfo.StateID;
                s.TaxID = gtiResp.SenderInfo.TaxID;
                
                //Set the main objects to the request.
                request.RequesterInfo = ri;
                request.Beneficiary = b;
                request.Sender = s;               
                request.TellerDrawerInstanceID = 17;
                request.OrderID = 0;
                request.OrderPIN = pin;
                request.OrderLookupCode = pin;
                request.RecAgentID = m_goldencrownAgentID;
                request.RecAgentLocID = Convert.ToInt32(gtiResp.RecAgentID);
                request.RecAgentBranch = "";
                request.RecAgentAddress = "";
                request.RecAgentCity = "";
                request.RecAgentState = recAgentState;
                request.RecAgentPostalCode = "";
                request.RecAgentCountry = gtiResp.CountryFrom;
                request.PayAgentID = callingAgentID;
                request.PayAgentLocID = callingAgentLocID;
                request.PayAgentBranch = gtiResp.PayAgentBranchName;
                request.PayAgentAddress = "";
                request.PayAgentCity = "";
                request.PayAgentState = callingState;
                request.PayAgentPostalCode = "";
                request.PayAgentCountry = callingCountry;
                request.PayAgentCountryID = -1;
                request.SendCurrency = gtiResp.PayoutAmount.CurrencyCode;//TODO: UNIT TEST:  gtiResp.SendAmount.CurrencyCode;
                request.SendAmount = gtiResp.PayoutAmount.Amount;//TODO: UNIT TEST:  gtiResp.SendAmount.Amount;
                request.SendCharge = 0.00m;//TODO: UNIT TEST:  gtiResp.SenderComission.Amount;
                request.PayoutCurrency = gtiResp.PayoutAmount.CurrencyCode;
                request.PayoutAmount = gtiResp.PayoutAmount.Amount;
                request.PayAgentCountryID = -1;
                request.PayoutMethodID = payMethodID;//TODO: UNIT TEST:  Convert.ToInt32(gtiResp.DeliveryMethod);
                request.CustomerRelationShip = "";
                request.CustomerRelationShipID = -1;
                request.TransferReasonID = -1;
                request.TransferReason = "";
                request.ApproverID = -1;
                request.ConvertedCurrency = "";
                request.ConvertedRate = Convert.ToDecimal(gtiResp.ExchangeRate);
                request.Override = false;

                //Make the Call:
                PayoutTransactionResponse ptResp = PayoutWebAPI_Client.PayoutTransactionRequest(request);
                //Check the Response:
                Assert.AreEqual(null, ptResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(1, ptResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(false, ptResp.ReturnInfo.AvailableForPayout);
                Assert.AreEqual(pin, ptResp.ConfirmationNumber);
                Assert.AreEqual(0.55m, ptResp.BeneficiaryFee.Amount);
                Assert.AreEqual("USD", ptResp.BeneficiaryFee.CurrencyCode);
                Assert.AreEqual(persistenceID, ptResp.PersistenceID);
                Assert.AreEqual("", ptResp.BeneficiaryPayout.Amount);
                Assert.AreEqual("EUR", ptResp.BeneficiaryPayout.CurrencyCode);
                Assert.AreEqual(true, ptResp.IsValid);
                Assert.AreEqual(0, ptResp.OrderID);
                Assert.AreEqual("", ptResp.OrderStatus);
                Assert.AreEqual("", ptResp.PayoutCommission.Amount);
                Assert.AreEqual("", ptResp.PayoutCommission.CurrencyCode);
                Assert.AreEqual(pin, ptResp.PIN);
                Assert.AreEqual(DateTime.UtcNow, ptResp.ResponseDateTimeUTC);
                if (ptResp.PayoutRequiredFields != null)
                {
                    foreach (PayoutFields f in ptResp.PayoutRequiredFields)
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


    }
}
