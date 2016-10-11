using CES.CoreApi.Payout.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CES.CoreApi.Payout.Service.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Controllers;
using AutoMapper;

using SimpleInjector;

namespace PayoutService.Test.ControllerTest
{
    [TestClass]
    class GetTrasactionInfo_Test
    {
        private readonly IRequestValidator _validator;
        private readonly IPayoutProcessor _processor;
        private readonly IMapper _mappingHelper;
        private const string DisableMultipleDNSEntriesInSANCertificate = @"Switch.System.IdentityModel.DisableMultipleDNSEntriesInSANCertificate";
        public GetTrasactionInfo_Test()
        {
            var container = new Container();
			CES.CoreApi.Payout.Api.DependenciesConfig.RegisterDependencies(container);
            _validator = container.GetInstance<IRequestValidator>();
            _processor = container.GetInstance<IPayoutProcessor>();
            _mappingHelper = container.GetInstance<IMapper>();

            
            AppContext.SetSwitch(DisableMultipleDNSEntriesInSANCertificate,true);
        }
        [TestMethod]
        public void GetInfoAndPayout_GC_RUtoES()
        {
            #region Arrange
            string pin = "00271569804";
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
            #endregion

            //GET TRANS INFO:
            GetTransactionInfoResponse gtiResp = null;
            try
            {
                #region Act
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = callingCountry;
                request.StateTo = callingState;
                //Make the Call:
                var controller = new PayoutController(_validator, _processor, _mappingHelper);

                gtiResp = controller.GetTransactionInfo(request) as GetTransactionInfoResponse;
                #endregion

                #region Assert
                //Check the Response:
                Assert.AreEqual(null, gtiResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(0, gtiResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, gtiResp.ReturnInfo.AvailableForPayout);
                //Checks a subset of all the fields.
                Assert.IsTrue(gtiResp.PersistenceID > 0);
                persistenceID = gtiResp.PersistenceID;//Set persistence ID to use in next call.
                Assert.AreEqual(new DateTime(2016, 7, 20, 0, 0, 0), gtiResp.TransferDate);
                Assert.AreEqual("Ready for Payout", gtiResp.TransferStatus);
                Assert.AreEqual(117.00m, gtiResp.PayoutAmount.Amount);
                Assert.AreEqual("EUR", gtiResp.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", gtiResp.CountryFrom);
                Assert.AreEqual(pin, gtiResp.OrderPIN);
                Assert.AreEqual("Cash Pickup", gtiResp.DeliveryMethod);
                //Assert.AreEqual(5.85m, gtiResp.BeneficiaryTax);
                //Assert.AreEqual(111.15m, gtiResp.NetAmount);
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
                #endregion 
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }

        [TestMethod]
        public void GetInfoAndPayout_GC_RUtoUS()
        {
            #region Arrange
            string pin = "00316746292";
            int callingAgentID = 39314611;
            int callingAgentLocID = 52651211;
            string locale = "en-us";
            string callingCountry = "US";
            string callingState = "CA";
            int persistenceID = 0;

            //Required Fields for payout not returned in info call:
            string benCountry = "US";
            string benIDIssuedCountry = "RU";
            string benIDType = "Passport";
            string benIDNumber = "1234567890";
            string benIDIssuedBy = "RU";
            string benNationality = "RU";
            DateTime benDOB = new DateTime(1980, 05, 15);
            DateTime benIDExpireDate = new DateTime(2020, 05, 15);
            DateTime benIDIssuedDate = new DateTime(2006, 05, 15);
            string benPostalCode = "90620";
            int benCityID = -1;
            int benStateID = -1;
            int payMethodID = 1;

            string recAgentState = "CA";

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
            #endregion
            //GET TRANS INFO:
            GetTransactionInfoResponse gtiResp = null;
            try
            {
                #region Act
                //Format request:
                GetTransactionInfoRequest request = new GetTransactionInfoRequest();
                request.PersistenceID = persistenceID;
                request.RequesterInfo = ri;
                request.OrderPIN = pin;
                request.OrderID = 0;
                request.CountryTo = callingCountry;
                request.StateTo = callingState;
                //Make the Call:
                var controller = new PayoutController(_validator, _processor, _mappingHelper);
                #endregion

                #region Assert
                gtiResp = controller.GetTransactionInfo(request) as GetTransactionInfoResponse;
                //Check the Response:
                Assert.AreEqual(null, gtiResp.ReturnInfo.ErrorMessage);
                Assert.AreEqual(0, gtiResp.ReturnInfo.ErrorCode);
                Assert.AreEqual(true, gtiResp.ReturnInfo.AvailableForPayout);
                //Checks a subset of all the fields.
                Assert.IsTrue(gtiResp.PersistenceID > 0);
                persistenceID = gtiResp.PersistenceID;//Set persistence ID to use in next call.
                Assert.AreEqual(new DateTime(2016, 7, 20, 0, 0, 0), gtiResp.TransferDate);
                Assert.AreEqual("Ready for Payout", gtiResp.TransferStatus);
                Assert.AreEqual(109.00m, gtiResp.PayoutAmount.Amount);
                Assert.AreEqual("USD", gtiResp.PayoutAmount.CurrencyCode);
                Assert.AreEqual("RU", gtiResp.CountryFrom);
                Assert.AreEqual(pin, gtiResp.OrderPIN);
                Assert.AreEqual("Cash Pickup", gtiResp.DeliveryMethod);
                //Assert.AreEqual(5.85m, gtiResp.BeneficiaryTax);
                //Assert.AreEqual(111.15m, gtiResp.NetAmount);
                Assert.AreEqual("NOLAT  ADELA ", gtiResp.SenderInfo.Name);
                Assert.AreEqual("NOLAT", gtiResp.SenderInfo.FirstName);
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
                #endregion
            }
            catch (Exception e)
            {
                Assert.AreEqual(1, e.Message);
            }
        }
    }
}
