using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Attributes;
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
    public class GoogleUrlBuilderUnitTests
    {
        private Mock<IConfigurationProvider> _configurationProvider;
        private Mock<IAddressQueryBuilder> _addressQueryBuilder;
        private Mock<IGooglePushPinParameterProvider> _pushPinParameterProvider;
        private Mock<ICorrectImageSizeProvider> _imageSizeProvider;

        private const string GoogleAddressAutocompleteUrlTemplateConfig = "GoogleAddressAutocompleteUrlTemplate";
        private const string GoogleLicenseKeyConfig = "GoogleLicenseKey";
        private const string GoogleAddressGeocodeAndVerificationUrlTemplateConfig = "GoogleAddressGeocodeAndVerificationUrlTemplate";
        private const string GoogleReverseGeocodePointUrlTemplateConfig = "GoogleReverseGeocodePointUrlTemplate";
        private const string GoogleMappingUrlTemplateConfig = "GoogleMappingUrlTemplate";

        private const string GoogleAddressGeocodeAndVerificationUrlTemplate = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}";
        private const string GoogleAddressAutocompleteUrlTemplate = "http://maps.googleapis.com/maps/api/geocode/xml?address={0}{1}";
        private const string GoogleReverseGeocodePointUrlTemplate = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}{2}";
        private const string GoogleMappingUrlTemplate = "http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom={2}&size={3}x{4}&format={5}&maptype={6}{7}{8}";
        private const string GoogleEmptyLicenseKey = "";
        private const string GoogleNonEmptyLicenseKey = "some_license";

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
            _pushPinParameterProvider = new Mock<IGooglePushPinParameterProvider>();
            _imageSizeProvider = new Mock<ICorrectImageSizeProvider>();
        }

        [TestMethod]
        public void Constructor_ConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
               SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }

        [TestMethod]
        public void Constructor_AddressQueryBuilderIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( null, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
               SubSystemError.GeneralRequiredParameterIsUndefined, "addressQueryBuilder");
        }

        [TestMethod]
        public void Constructor_PushPinParameterProviderIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, null, _imageSizeProvider.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "pushPinParameterProvider");
        }

        [TestMethod]
        public void Constructor_ImageSizeProviderIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "imageSizeProvider");
        }

        [TestMethod]
        public void Constructor_LicenseKeyIsNull_NoExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig)).Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(GoogleAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig)).Returns(GoogleReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(GoogleMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(string.Empty);

            ExceptionHelper.CheckHappyPath(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object));
        }

        [TestMethod]
        public void Constructor_LicenseKeyIsNotNull_NoExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig)).Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(GoogleAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig)).Returns(GoogleReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(GoogleMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleNonEmptyLicenseKey);

            ExceptionHelper.CheckHappyPath(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object));
        }

        [TestMethod]
        public void Constructor_AddressAutocompleteUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig)).Returns(string.Empty);
                _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(GoogleAddressGeocodeAndVerificationUrlTemplate);
                _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig)).Returns(GoogleReverseGeocodePointUrlTemplate);
                _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(GoogleMappingUrlTemplate);
                _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
                SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Google, GoogleAddressAutocompleteUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_AddressGeocodeAndVerificationUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig)).Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig)).Returns(GoogleReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(GoogleMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
                SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Google, GoogleAddressGeocodeAndVerificationUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_ReverseGeocodePointUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig)).Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(GoogleAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(GoogleMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
                SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Google, GoogleReverseGeocodePointUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_MappingUrlTemplateIsNull_ExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig)).Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(GoogleAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig)).Returns(GoogleReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(string.Empty);
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckException(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object),
                SubSystemError.GeolocationUrlTemplateNotFound, DataProviderType.Google, GoogleMappingUrlTemplateConfig);
        }

        [TestMethod]
        public void Constructor_SuccessPath_NoExceptionRaised()
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig)).Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig)).Returns(GoogleAddressGeocodeAndVerificationUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig)).Returns(GoogleReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(GoogleMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(GoogleEmptyLicenseKey);

            ExceptionHelper.CheckHappyPath(() => new GoogleUrlBuilder( _addressQueryBuilder.Object, _pushPinParameterProvider.Object, _imageSizeProvider.Object));
        }

        [TestMethod]
        public void BuildUrl_ReverseGeocodePointUrlLicenseKeyIsNull_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();
            var model = GetLocationModel();

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                GoogleReverseGeocodePointUrlTemplate,
                model.Latitude,
                model.Longitude,
                GoogleEmptyLicenseKey);

            var result = builder.BuildUrl(model);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildUrl_ReverseGeocodePointUrlLicenseKeyIsNotNull_ProperlyFormatted()
        {
            var builder = GetUrlBuilder(false);
            var model = GetLocationModel();

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                GoogleReverseGeocodePointUrlTemplate,
                model.Latitude,
                model.Longitude,
                string.Empty);

            expected = expected + "&key=" + GoogleNonEmptyLicenseKey;

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
        //        GoogleAddressAutocompleteUrlTemplate,
        //        query,
        //        GoogleEmptyLicenseKey);

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

            var addressFormatted = string.Join(",",
                model.Address1,
                model.Address2,
                model.City,
                model.AdministrativeArea,
                model.PostalCode,
                model.Country);

            var expected = string.Format(CultureInfo.InvariantCulture,
                GoogleAddressGeocodeAndVerificationUrlTemplate,
                HttpUtility.UrlEncode(addressFormatted),
                GoogleEmptyLicenseKey);
            
            var result = builder.BuildUrl(model);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildUrl_MappingUrlTemplate_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();

            var locationModel = GetLocationModel();
            var mapSizeModel = new MapSizeModel { Height = Height, Width = Width };
            var mapOutputParametersModel = new MapOutputParametersModel
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
                GoogleMappingUrlTemplate,
                locationModel.Latitude,
                locationModel.Longitude,
                mapOutputParametersModel.ZoomLevel,
                Width,
                Height,
                mapOutputParametersModel.ImageFormat.GetAttributeValue<GoogleImageFormatAttribute, string>(x => x.ImageFormat),
                mapOutputParametersModel.MapStyle.GetAttributeValue<GoogleMapTypeAttribute, string>(x => x.MapType),
                string.Empty,
                GoogleEmptyLicenseKey);

            var result = builder.BuildUrl(locationModel, mapSizeModel, mapOutputParametersModel, null);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BuildUrl_FormattedAddressGeocodeAndVerificationUrlTemplate_ProperlyFormatted()
        {
            var builder = GetUrlBuilder();

            var expected = string.Format(
                CultureInfo.InvariantCulture,
                GoogleAddressAutocompleteUrlTemplate,
                HttpUtility.UrlEncode(FormattedAddress),
                GoogleEmptyLicenseKey);

            var result = builder.BuildUrl(FormattedAddress);

            Assert.AreEqual(expected, result);
        }

        private GoogleUrlBuilder GetUrlBuilder(bool isKeyNull = true)
        {
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressAutocompleteUrlTemplateConfig))
                .Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleAddressGeocodeAndVerificationUrlTemplateConfig))
                .Returns(GoogleAddressAutocompleteUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleReverseGeocodePointUrlTemplateConfig))
                .Returns(GoogleReverseGeocodePointUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleMappingUrlTemplateConfig)).Returns(GoogleMappingUrlTemplate);
            _configurationProvider.Setup(p => p.Read<string>(GoogleLicenseKeyConfig)).Returns(isKeyNull ? GoogleEmptyLicenseKey : GoogleNonEmptyLicenseKey);

            var builder = new GoogleUrlBuilder( _addressQueryBuilder.Object,
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
