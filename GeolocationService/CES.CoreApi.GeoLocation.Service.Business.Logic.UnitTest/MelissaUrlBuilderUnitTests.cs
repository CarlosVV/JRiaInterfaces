using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class MelissaUrlBuilderUnitTests
    {
        private Mock<IConfigurationProvider> _configurationProvider;

        private const string MelissaDataAddressAutocompleteUrlTemplateConfig = "MelissaDataAddressAutocompleteUrlTemplate";
        private const string MelissaDataLicenseKeyConfig = "MelissaDataLicenseKey";
        private const string MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplateConfig = "MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate";
        private const string MelissaDataAddressGeocodeAndVerificationUrlTemplateConfig = "MelissaDataAddressGeocodeAndVerificationUrlTemplate";

        private const string MelissaDataAddressGeocodeAndVerificationUrlTemplate = "http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&a2={2}&loc={3}&admarea{4}&postal={5}&ctry={6}";
        private const string MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate = "http://address.melissadata.net/V3/WEB/GlobalAddress/doGlobalAddress?format=xml&id={0}&a1={1}&maxrecords=1";
        private const string MelissaDataAddressAutocompleteUrlTemplate = "http://expressentry.melissadata.net/web/GlobalExpressAddress?id={0}&format=xml&address1={1}&administrativearea={2}&Country={3}&maxrecords={4}";
        private const string MelissaDataLicenseKey = "109099452";

        private const string Address1 = "Address1";
        private const string Address2 = "Address2";
        private const string ValidCountry = "US";
        private const string AdministrativeArea = "CA";
        private const string City = "Buena Park";
        private const string PostalCode = "90620";
        private const int MaxRecords = 15;
        private const string FormattedAddress = "6565 Knott Ave., Buena Park, CA 90620";

        private const double LatitudeValid = 36.3456;
        private const double LongitudeValid = -170.5453;

        [TestInitialize]
        public void Setup()
        {
            _configurationProvider = new Mock<IConfigurationProvider>();
        }

        [TestMethod]
        public void Constructor_ConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new MelissaUrlBuilder(null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }
        
        [TestMethod]
        public void Constructor_LicenseKeyIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataLicenseKeyConfig)).Returns(string.Empty);

            ExceptionHelper.CheckException(() => new MelissaUrlBuilder(_configurationProvider.Object),
                SubSystemError.GeolocationLicenseKeyNotFound, DataProviderType.MelissaData);
        }

        [TestMethod]
        public void Constructor_AddressAutocompleteUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressAutocompleteUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(MelissaDataAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataLicenseKeyConfig)).Returns(MelissaDataLicenseKey);

            ExceptionHelper.CheckException(() => new MelissaUrlBuilder(_configurationProvider.Object),
                SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.MelissaData, MelissaDataAddressAutocompleteUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_FormattedAddressGeocodeAndVerificationUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressAutocompleteUrlTemplateConfig)).Returns(MelissaDataAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(MelissaDataAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataLicenseKeyConfig)).Returns(MelissaDataLicenseKey);

            ExceptionHelper.CheckException(() => new MelissaUrlBuilder(_configurationProvider.Object),
                SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.MelissaData, MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_AddressGeocodeAndVerificationUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressAutocompleteUrlTemplateConfig)).Returns(MelissaDataAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataLicenseKeyConfig)).Returns(MelissaDataLicenseKey);

            ExceptionHelper.CheckException(() => new MelissaUrlBuilder(_configurationProvider.Object),
               SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.MelissaData, MelissaDataAddressGeocodeAndVerificationUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_SuccessPath_NoExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressAutocompleteUrlTemplateConfig)).Returns(MelissaDataAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(MelissaDataAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataLicenseKeyConfig)).Returns(MelissaDataLicenseKey);

            ExceptionHelper.CheckHappyPath(() => new MelissaUrlBuilder(_configurationProvider.Object));
        }

        [TestMethod]
        public void BuildUrl_ReverseGeocodePointUrl_ExceptionRaised()
        {
            var builder = GetUrlBuilder();
            var model = GetLocationModel();

            ExceptionHelper.CheckException(() => builder.BuildUrl(model),
               SubSystemError.GeolocationReverseGeocodingIsNotSupported, DataProviderType.MelissaData);
        }

        //[TestMethod]
        //public void BuildUrl_AddressAutocompleteUrl_ProperlyFormatted()
        //{
        //    var builder = GetUrlBuilder();

        //    var expected = string.Format(
        //        CultureInfo.InvariantCulture,
        //        MelissaDataAddressAutocompleteUrlTemplate,
        //        MelissaDataLicenseKey,
        //        HttpUtility.UrlEncode(Address1),
        //        HttpUtility.UrlEncode(AdministrativeArea),
        //        HttpUtility.UrlEncode(ValidCountry),
        //        MaxRecords);

        //    var result = builder.BuildUrl(Address1, AdministrativeArea, ValidCountry, MaxRecords);

        //    Assert.AreEqual(expected, result);
        //}

        [TestMethod]
        public void BuildUrl_AddressGeocodeAndVerificationUrl_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();

            var model = new AddressModel
            {
                Address1 = Address1,
                Address2 = Address2,
                AdministrativeArea = AdministrativeArea,
                City = City,
                Country = ValidCountry,
                PostalCode = PostalCode
            };

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                MelissaDataAddressGeocodeAndVerificationUrlTemplate,
                MelissaDataLicenseKey,
                HttpUtility.UrlEncode(model.Address1),
                HttpUtility.UrlEncode(model.Address2),
                HttpUtility.UrlEncode(model.City),
                HttpUtility.UrlEncode(model.AdministrativeArea),
                HttpUtility.UrlEncode(model.PostalCode),
                HttpUtility.UrlEncode(model.Country));

            var result = builder.BuildUrl(model);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildUrl_FormattedAddressGeocodeAndVerificationUrlTemplate_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate,
                MelissaDataLicenseKey,
                HttpUtility.UrlEncode(FormattedAddress));

            var result = builder.BuildUrl(FormattedAddress);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildUrl_MappingUrlTemplate_ExceptionRaised()
        {
            var builder = GetUrlBuilder();

            ExceptionHelper.CheckException(() => builder.BuildUrl(It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>()),
               SubSystemError.GeolocationMappingIsNotSupported, DataProviderType.MelissaData);
        }

        private MelissaUrlBuilder GetUrlBuilder()
        {
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressAutocompleteUrlTemplateConfig))
                .Returns(MelissaDataAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplateConfig))
                .Returns(MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataAddressGeocodeAndVerificationUrlTemplateConfig))
                .Returns(MelissaDataAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(MelissaDataLicenseKeyConfig)).Returns(MelissaDataLicenseKey);

            var builder = new MelissaUrlBuilder(_configurationProvider.Object);
            return builder;
        }


        private static LocationModel GetLocationModel()
        {
            var model = new LocationModel
            {
                Latitude = LatitudeValid,
                Longitude = LongitudeValid
            };
            return model;
        }
    }
}
