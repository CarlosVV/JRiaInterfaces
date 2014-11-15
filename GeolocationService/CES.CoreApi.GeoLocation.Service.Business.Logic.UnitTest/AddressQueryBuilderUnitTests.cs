using System.Globalization;
using System.Web;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class AddressQueryBuilderUnitTests
    {
        private const string Address1 = "Address1";
        private const string Address2 = "Address2";
        private const string ValidCountry = "US";
        private const string AdministrativeArea = "CA";
        private const string QueryWithAdministrativeAreaTemplate = "{0} {1} {2}";
        private const string QueryWithoutAdministrativeAreaTemplate = "{0} {1}";
        private const string CompositeAddressTemplate = "{0} {1}";

        [TestInitialize]
        public void Setup()
        {
           
        }

        [TestMethod]
        [ExpectedException(typeof(CoreApiException))]
        public void Build_AddressIsNull_ExceptionRaised()
        {
            new AddressQueryBuilder().Build(string.Empty, AdministrativeArea, ValidCountry);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreApiException))]
        public void Build_CountryIsNull_ExceptionRaised()
        {
            new AddressQueryBuilder().Build(Address1, AdministrativeArea, string.Empty);
        }

        [TestMethod]
        public void Build_QueryWithoutAdministrativeArea_CorrectlyFormattedAndEncoded()
        {
            var expected = string.Format(CultureInfo.InvariantCulture, QueryWithoutAdministrativeAreaTemplate, Address1, ValidCountry);
            expected = HttpUtility.UrlPathEncode(expected);

            var result = new AddressQueryBuilder().Build(Address1, string.Empty, ValidCountry);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Build_QueryWithAdministrativeArea_CorrectlyFormattedAndEncoded()
        {
            var expected = string.Format(CultureInfo.InvariantCulture, QueryWithAdministrativeAreaTemplate, Address1,
                AdministrativeArea, ValidCountry);
            expected = HttpUtility.UrlPathEncode(expected);
            
            var result = new AddressQueryBuilder().Build(Address1, AdministrativeArea, ValidCountry);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreApiException))]
        public void Build_Address1IsNull_ExceptionRaised()
        {
            new AddressQueryBuilder().Build(string.Empty, Address2);
        }

        [TestMethod]
        public void Build_Address2IsNull_CorrectlyFormatted()
        {
            var result = new AddressQueryBuilder().Build(Address1, string.Empty);

            Assert.AreEqual(Address1, result);
        }

        [TestMethod]
        public void Build_BothAddressesProvided_CorrectlyFormatted()
        {
            var expected = string.Format(CultureInfo.InvariantCulture, CompositeAddressTemplate, Address1, Address2);

            var result = new AddressQueryBuilder().Build(Address1, Address2);

            Assert.AreEqual(expected, result);
        }
    }
}
