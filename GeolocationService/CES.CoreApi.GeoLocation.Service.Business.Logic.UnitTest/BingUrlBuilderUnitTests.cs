using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class BingUrlBuilderUnitTests
    {
        private Mock<IConfigurationProvider> _configurationProvider;
        private Mock<IAddressQueryBuilder> _addressQueryBuilder;
        private Mock<IBingPushPinParameterProvider> _pushPinParameterProvider;
        private Mock<ICorrectImageSizeProvider> _imageSizeProvider;

        private const string BingAddressAutocompleteUrlTemplateConfig = "BingAddressAutocompleteUrlTemplate";
        private const string BingLicenseKeyConfigurationName = "BingLicenseKey";
        private const string BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig = "BingFormattedAddressGeocodeAndVerificationUrlTemplate";
        private const string BingAddressGeocodeAndVerificationUrlTemplateConfig = "BingAddressGeocodeAndVerificationUrlTemplate";
        private const string BingReverseGeocodePointUrlTemplateConfig = "BingReverseGeocodePointUrlTemplate";
        private const string BingMappingUrlTemplateConfig = "BingMappingUrlTemplate";

        private const string BingAddressGeocodeAndVerificationUrlTemplate = "http://dev.virtualearth.net/REST/v1/Locations?CountryRegion={0}&adminDistrict={1}&locality={2}&postalCode={3}&addressLine={4}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={5}";
        private const string BingFormattedAddressGeocodeAndVerificationUrlTemplate = "http://dev.virtualearth.net/REST/v1/Locations?q={0}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={1}";
        private const string BingAddressAutocompleteUrlTemplate = "http://dev.virtualearth.net/REST/v1/Locations/{0}?o=xml&userIp=127.0.0.1&maxResults={1}&include=ciso2&key={2}";
        private const string BingReverseGeocodePointUrlTemplate = "http://dev.virtualearth.net/REST/v1/Locations/{0},{1}?o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={2}";
        private const string BingMappingUrlTemplate = "http://dev.virtualearth.net/REST/v1/Imagery/Map/{0}/{1},{2}/{3}?mapSize={4},{5}&format={6}{7}&key={8}";
        private const string BingLicenseKey = "AkuAfUPJx-izRLvlNf5GXBWPybHdcFwh34U5krgE2RsGiQs9xwLpUvpvPo8yceiI";

        private const string Address1 = "Address1";
        private const string Address2 = "Address2";
        private const string ValidCountry = "US";
        private const string AdministrativeArea = "CA";
        private const string City = "Buena Park";
        private const string PostalCode = "90620";
        private const int MaxRecords = 15;
        private const string FormattedAddress = "6565 Knott Ave., Buena Park, CA 90620";
        private const int Width = 100;
        private const int Height = 100;

        private const double LatitudeValid = 36.3456;
        private const double LongitudeValid = -170.5453;

        [TestInitialize]
        public void Setup()
        {
            _configurationProvider = new Mock<IConfigurationProvider>();
            _addressQueryBuilder = new Mock<IAddressQueryBuilder>();
            _pushPinParameterProvider = new Mock<IBingPushPinParameterProvider>();
            _imageSizeProvider = new Mock<ICorrectImageSizeProvider>();
        }

        [TestMethod]
        public void Constructor_ConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new BingUrlBuilder(null, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }

        [TestMethod]
        public void Constructor_AddressQueryBuilderIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, null, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "addressQueryBuilder");
        }

        [TestMethod]
        public void Constructor_PushPinParameterProviderIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, null, _imageSizeProvider.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "pushPinParameterProvider");
        }

        [TestMethod]
        public void Constructor_ImageSizeProviderIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "imageSizeProvider");
        }

        [TestMethod]
        public void Constructor_LicenseKeyIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(string.Empty);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
               SubSystemError.GeolocationLicenseKeyNotFound, DataProviderType.Bing);
        }

        [TestMethod]
        public void Constructor_AddressAutocompleteUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingAddressAutocompleteUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingReverseGeocodePointUrlTemplateConfig)).Returns(BingReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingMappingUrlTemplateConfig)).Returns(BingMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
               SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Bing, BingAddressAutocompleteUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_FormattedAddressGeocodeAndVerificationUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingAddressAutocompleteUrlTemplateConfig)).Returns(BingAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(BingAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingReverseGeocodePointUrlTemplateConfig)).Returns(BingReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingMappingUrlTemplateConfig)).Returns(BingMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
              SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Bing, BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_AddressGeocodeAndVerificationUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingAddressAutocompleteUrlTemplateConfig)).Returns(BingAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(BingReverseGeocodePointUrlTemplateConfig)).Returns(BingReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingMappingUrlTemplateConfig)).Returns(BingMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
              SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Bing, BingAddressGeocodeAndVerificationUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_ReverseGeocodePointUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingAddressAutocompleteUrlTemplateConfig)).Returns(BingAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingReverseGeocodePointUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(BingMappingUrlTemplateConfig)).Returns(BingMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
              SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Bing, BingReverseGeocodePointUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_MappingUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingAddressAutocompleteUrlTemplateConfig)).Returns(BingAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingReverseGeocodePointUrlTemplateConfig)).Returns(BingReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingMappingUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckException(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
              SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Bing, BingMappingUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_SuccessPath_NoExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingAddressAutocompleteUrlTemplateConfig)).Returns(BingAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(BingAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingReverseGeocodePointUrlTemplateConfig)).Returns(BingReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingMappingUrlTemplateConfig)).Returns(BingMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            ExceptionHelper.CheckHappyPath(() => new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object));
        }

        [TestMethod]
        public void BuildUrl_ReverseGeocodePointUrl_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();

            var model = GetLocationModel();

            var expected = string.Format(
                CultureInfo.InvariantCulture, 
                BingReverseGeocodePointUrlTemplate,
                model.Latitude, 
                model.Longitude, 
                BingLicenseKey);

            var result = builder.BuildUrl(model);

            Assert.AreEqual(expected, result);
        }

        //[TestMethod]
        //public void BuildUrl_AddressAutocompleteUrl_ProperlyFormatted()
        //{
        //    var builder = GetUrlBuilder();

        //    var query = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", Address1, AdministrativeArea, ValidCountry);
        //    query = HttpUtility.UrlPathEncode(query);

        //    var expected = string.Format(
        //        CultureInfo.InvariantCulture,
        //        BingAddressAutocompleteUrlTemplate,
        //        query,
        //        MaxRecords,
        //        BingLicenseKey);

        //    _addressQueryBuilder.Setup(p => p.Build(Address1, AdministrativeArea, ValidCountry)).Returns(query);

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

            var query = string.Format(CultureInfo.InvariantCulture, "{0} {1}", Address1, Address2);

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                BingAddressGeocodeAndVerificationUrlTemplate,
                HttpUtility.UrlEncode(model.Country),
                HttpUtility.UrlEncode(model.AdministrativeArea),
                HttpUtility.UrlEncode(model.City),
                HttpUtility.UrlEncode(model.PostalCode),
                HttpUtility.UrlEncode(query),
                BingLicenseKey);

            _addressQueryBuilder.Setup(p => p.Build(Address1, Address2)).Returns(query);

            var result = builder.BuildUrl(model);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildUrl_FormattedAddressGeocodeAndVerificationUrlTemplate_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                BingFormattedAddressGeocodeAndVerificationUrlTemplate,
                HttpUtility.UrlEncode(FormattedAddress),
                BingLicenseKey);

            var result = builder.BuildUrl(FormattedAddress);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildUrl_MappingUrlTemplate_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();

            var locationModel = GetLocationModel();
            var mapSizeModel = new MapSizeModel { Height = Height, Width = Width };
            var mapOutputParametersModel = new MapOutputParametersModel()
            {
                ImageFormat = ImageFormat.Jpeg,
                MapStyle = MapStyle.Road,
                ZoomLevel = 20
            };

            _pushPinParameterProvider.Setup(p => p.GetPushPinParameter(It.IsAny<ICollection<PushPinModel>>())).Returns(string.Empty);
            _imageSizeProvider.Setup(p => p.GetCorrectImageSize(It.IsAny<DataProviderType>(), ImageDimension.Width, It.IsAny<int>())).Returns(mapSizeModel.Width);
            _imageSizeProvider.Setup(p => p.GetCorrectImageSize(It.IsAny<DataProviderType>(), ImageDimension.Height, It.IsAny<int>())).Returns(mapSizeModel.Height);

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                BingMappingUrlTemplate,
                mapOutputParametersModel.MapStyle,
                locationModel.Latitude,
                locationModel.Longitude,
                mapOutputParametersModel.ZoomLevel,
                Width,
                Height,
                mapOutputParametersModel.ImageFormat,
                string.Empty,
                BingLicenseKey);

            var result = builder.BuildUrl(locationModel, mapSizeModel, mapOutputParametersModel, null);

            Assert.AreEqual(expected, result);
        }

        private BingUrlBuilder GetUrlBuilder()
        {
            _configurationProvider.Setup(p => p.Read<string>(BingAddressAutocompleteUrlTemplateConfig))
                .Returns(BingAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingFormattedAddressGeocodeAndVerificationUrlTemplateConfig))
                .Returns(BingFormattedAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingAddressGeocodeAndVerificationUrlTemplateConfig))
                .Returns(BingAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingReverseGeocodePointUrlTemplateConfig))
                .Returns(BingReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingMappingUrlTemplateConfig)).Returns(BingMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(BingLicenseKeyConfigurationName)).Returns(BingLicenseKey);

            var builder = new BingUrlBuilder(_configurationProvider.Object, _addressQueryBuilder.Object,
                _pushPinParameterProvider.Object, _imageSizeProvider.Object);
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
