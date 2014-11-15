using System.Linq;
using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class GoogleAddressParserUnitTests
    {
        [TestMethod]
        public void ParseAddress_AddressElementsIsNull_NullReturned()
        {
            var result = new GoogleAddressParser().ParseAddress(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ParseAddress_RealGoogleResponse_AllAddressElementsParsedCorrectly()
        {
            //Real response from Google
            var xDocument = XDocument.Parse(GoogleResponseParserHelper.ValidateAddressRawResponse);

            if (xDocument.Root == null)
                Assert.Fail("XML document root is NULL.");

            // ReSharper disable PossibleNullReferenceException
            var addressElement = xDocument.Root.Elements(GoogleConstants.Result).FirstOrDefault();
            // ReSharper restore PossibleNullReferenceException

            var result = new GoogleAddressParser().ParseAddress(addressElement);

            Assert.IsNotNull(result);
            Assert.AreEqual("1445 Brett Pl", result.Address1);
            Assert.IsNull(result.Address2);
            Assert.AreEqual("108", result.UnitOrApartment);
            Assert.IsNull(result.UnitsOrApartments);
            Assert.AreEqual("CA", result.AdministrativeArea);
            Assert.AreEqual("US", result.Country);
            Assert.AreEqual("Los Angeles", result.City);
            Assert.AreEqual("90732", result.PostalCode);
            Assert.AreEqual("1445 Brett Place #108, San Pedro, CA 90732, USA", result.FormattedAddress);
        }
    }
}
