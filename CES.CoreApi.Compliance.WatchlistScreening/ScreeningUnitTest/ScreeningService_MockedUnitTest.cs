using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CES.CoreApi.Compliance.Screening.Services;
using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Models.DTO;
using Moq;
using CES.CoreApi.Compliance.Screening.Repositories;
using CES.CoreApi.Compliance.Screening.Providers;
using CES.CoreApi.Compliance.Screening;

namespace ScreeningUnitTest
{
    [TestClass]
    public class ScreeningService_MockedUnitTest
    {
        private ScreeningService service = null;    

        public ScreeningService_MockedUnitTest()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [TestInitialize]
        void Setup_Mocked_Objects()
        {
            // setup mocked repository
            var moqRepo = new Mock<WatchlistRuleRepository>() { CallBase = true };
            moqRepo.Setup(m => m.GetAllScreeningRules(string.Empty,string.Empty)).Returns(new Rule[] {
                new Rule() {
                    CountryTo = "AF",
                    ContryFrom = "US",
                    BusinessUnit = "anyBussUnit",
                    SearchDef = "anySearchDefId"
                },
                new Rule() {
                    CountryTo = "ID",
                    ContryFrom = "US",
                    BusinessUnit = "anyBussUnit",
                    SearchDef = "anySearchDefId"
                }
            });
            moqRepo.Setup(m => m.CreateReviewAlert(new ReviewAlertCreateRequest()));
            var moqProvider = new Mock<WatchlistScreeningProvider>() { CallBase = true };
            moqProvider.Setup(m => m.AllowTransaction(new Request(),new Party(),new Rule())).Returns(new ActimizeResponse());
            moqProvider.Setup(m => m.AllowTransaction(new Request(), new Party(), new Rule())).Returns(new ActimizeResponse());
  
            this.service = new ScreeningService(moqRepo.Object, moqProvider.Object);
        }

        [TestMethod]
        void Screening_With_Hit_Should_Returns_Success()
        {

            // Arrange
            var sendAmount = 2000.00;
            var countryFrom = "US";
            var countryTo = "IQ";
            var parties = new Party[] {
                new Party()
                {
                    FirstName = "Sadi",
                    MiddleName = "Tuma",
                    LastName1="Abbas",
                    Type = PartyType.Customer
                }
            };

            var request = new Request() { SendAmount = sendAmount, CountryFrom = countryFrom, CountryTo = countryTo, Parties = parties };

            // Action
            var result = this.service.ScreeningTransaction(request);

            // Assert
            Assert.IsTrue(result.Code == 0);
            Assert.IsTrue(result.LegalHold);
        }

        [TestMethod]
        void Screening_Without_Hit_Should_Returns_Success()
        {
            // Arrange
            var sendAmount = 500.00;
            var countryFrom = "US";
            var countryTo = "AF";
            var parties = new Party[] {
                new Party()
                {
                    FirstName = "bhudani",
                    LastName2 = "laden",
                    Type = PartyType.Customer
                }
            };

            var request = new Request() { SendAmount=sendAmount,CountryFrom=countryFrom,CountryTo =countryTo, Parties=parties};

            // Action
            var result = this.service.ScreeningTransaction(request);

            // Assert
            Assert.IsTrue(result.Code == 0);
            Assert.IsTrue(!result.LegalHold);
        }
    }
}
