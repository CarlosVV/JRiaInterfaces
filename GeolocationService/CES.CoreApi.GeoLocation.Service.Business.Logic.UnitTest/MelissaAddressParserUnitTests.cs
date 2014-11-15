using System.Linq;
using System.Xml.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class MelissaAddressParserUnitTests
    {
        private const string Country = "US";

        [TestMethod]
        public void ParseAddress_AddressElementsIsNull_NullReturned()
        {
            var result = new MelissaAddressParser().ParseAddress(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ParseAddress_CountryIsNullAndIsAutocompleteServiceIsFalse_AllAddressElementsParsedCorrectly()
        {
            //Real response from Google
            var xDocument = XDocument.Parse(MelissaResponseParserHelper.ValidateAddressRawResponse);

            if (xDocument.Root == null)
                Assert.Fail("XML document root is NULL.");

            // ReSharper disable PossibleNullReferenceException
            var addressElement = (from records in xDocument.Root.Elements(MelissaResponseParserHelper.XNamespace + MelissaConstants.Records)
            // ReSharper restore PossibleNullReferenceException
                                  from responseRecord in records.Elements(MelissaResponseParserHelper.XNamespace + MelissaConstants.ResponseRecord)
                           select responseRecord).FirstOrDefault();

            var result = new MelissaAddressParser().ParseAddress(addressElement, MelissaResponseParserHelper.XNamespace);

            Assert.IsNotNull(result);
            Assert.AreEqual("1445 Brett Pl Unit 108", result.Address1);
            Assert.IsNull(result.Address2);
            Assert.AreEqual("Unit 108", result.UnitOrApartment);
            Assert.IsNull(result.UnitsOrApartments);
            Assert.AreEqual("CA", result.AdministrativeArea);
            Assert.AreEqual("US", result.Country);
            Assert.AreEqual("San Pedro", result.City);
            Assert.AreEqual("90732-5111", result.PostalCode);
            Assert.AreEqual("1445 BRETT PL UNIT 108;SAN PEDRO CA  90732-5111", result.FormattedAddress);
        }

        [TestMethod]
        public void ParseAddress_CountryIsNotNullAndIsAutocompleteServiceIsTrue_AllAddressElementsParsedCorrectly()
        {
            //Real response from Google
            var xDocument = XDocument.Parse(MelissaResponseParserHelper.AutoCompleteRawResponse);

            if (xDocument.Root == null)
                Assert.Fail("XML document root is NULL.");

            // ReSharper disable PossibleNullReferenceException
            var addressElement = (from results in xDocument.Root.Elements(MelissaConstants.Results) 
                                  from hint in results.Elements(MelissaConstants.ResultGlobal)
                                  // ReSharper restore PossibleNullReferenceException
                                  select hint.Element(MelissaConstants.Address)).FirstOrDefault();

          
            var result = new MelissaAddressParser().ParseAddress(addressElement, null, Country, true);

            Assert.IsNotNull(result);
            Assert.AreEqual("1445 Brett Pl", result.Address1);
            Assert.IsNull(result.Address2);
            Assert.IsNull(result.UnitOrApartment);
            Assert.IsNotNull(result.UnitsOrApartments);
            Assert.IsTrue(result.UnitsOrApartments.Count == 44);
            Assert.AreEqual("CA", result.AdministrativeArea);
            Assert.AreEqual("US", result.Country);
            Assert.AreEqual("SAN PEDRO", result.City);
            Assert.AreEqual("90732", result.PostalCode);
            Assert.AreEqual("1445 Brett Pl, SAN PEDRO CA 90732", result.FormattedAddress);
        }
    }
}
