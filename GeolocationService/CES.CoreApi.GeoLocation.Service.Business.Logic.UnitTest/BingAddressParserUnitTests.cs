using System.Linq;
using System.Xml.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class BingAddressParserUnitTests
    {
        [TestMethod]
        public void ParseAddress_AddressElementsIsNull_NullReturned()
        {
            var result = new BingAddressParser().ParseAddress(null, string.Empty);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ParseAddress_RealBingResponse_AllAddressElementsParsedCorrectly()
        {
            //Real response from Bing
            var xDocument = XDocument.Parse(BingResponseParserHelper.ValidateAddressRawResponse);

            if (xDocument.Root == null)
                Assert.Fail("XML document root is NULL.");

            var addressElement = (from resourceSet in
                // ReSharper disable PossibleNullReferenceException
                xDocument.Root.Element(BingResponseParserHelper.XNamespace + BingConstants.ResourceSets)
                    .Elements(BingResponseParserHelper.XNamespace + BingConstants.ResourceSet)
                // ReSharper restore PossibleNullReferenceException
                let estimatedTotal = resourceSet.GetValue<int>(BingConstants.EstimatedTotal, BingResponseParserHelper.XNamespace)
                where estimatedTotal > 0
                from resource in resourceSet.Elements(BingResponseParserHelper.XNamespace + BingConstants.Resources)
                from location in resource.Elements(BingResponseParserHelper.XNamespace + BingConstants.Location)
                let address = location.Element(BingResponseParserHelper.XNamespace + BingConstants.Address)
                select address)
                .FirstOrDefault();

            var result = new BingAddressParser().ParseAddress(addressElement, BingResponseParserHelper.XNamespace);
            
            Assert.IsNotNull(result);
            Assert.AreEqual("1445 Brett Pl", result.Address1);
            Assert.IsNull(result.Address2);
            Assert.IsNull(result.UnitOrApartment);
            Assert.IsNull(result.UnitsOrApartments);
            Assert.AreEqual("CA", result.AdministrativeArea);
            Assert.AreEqual("US", result.Country);
            Assert.AreEqual("San Pedro", result.City);
            Assert.AreEqual("90732", result.PostalCode);
            Assert.AreEqual("1445 Brett Pl, San Pedro, CA 90732", result.FormattedAddress);
        }

        [TestMethod]
        public void ParseAddress_WithNoNamespace_AllAddressElementsParsedCorrectly()
        {
            var xDocument = XDocument.Parse(BingResponseParserHelper.AddressHintWithNoNamespaceXml);
            var result = new BingAddressParser().ParseAddress(xDocument.Element(BingConstants.Address), string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual("1445 Brett Pl", result.Address1);
            Assert.IsNull(result.Address2);
            Assert.IsNull(result.UnitOrApartment);
            Assert.IsNull(result.UnitsOrApartments);
            Assert.AreEqual("CA", result.AdministrativeArea);
            Assert.AreEqual("US", result.Country);
            Assert.AreEqual("San Pedro", result.City);
            Assert.AreEqual("90732", result.PostalCode);
            Assert.AreEqual("1445 Brett Pl, San Pedro, CA 90732", result.FormattedAddress);
        }
    }
}
