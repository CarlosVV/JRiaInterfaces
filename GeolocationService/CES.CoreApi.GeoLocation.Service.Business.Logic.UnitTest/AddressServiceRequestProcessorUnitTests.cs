using System.Collections.Generic;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class AddressServiceRequestProcessorUnitTests
    {
        private Mock<ICountryConfigurationProvider> _configurationProvider;
        private Mock<IAddressVerificationDataProvider> _addressVerificationDataProvider;
        private Mock<IAddressAutocompleteDataProvider> _addressAutocompleteDataProvider;
        private Mock<IGeocodeServiceRequestProcessor> _geocodeServiceRequestProcessor;

        private const string Address1 = "Address1";
        private const string Address2 = "Address2";
        private const string Country = "US";
        private const string AdministrativeArea = "CA";
        private const string City = "Buena Park";
        private const string PostalCode = "90620";
        private const string FormattedAddress = "6565 Knott Ave., Buena Park, CA 90620";

        [TestInitialize]
        public void Setup()
        {
            _configurationProvider = new Mock<ICountryConfigurationProvider>();
            _addressAutocompleteDataProvider = new Mock<IAddressAutocompleteDataProvider>();
            _addressVerificationDataProvider = new Mock<IAddressVerificationDataProvider>();
            _geocodeServiceRequestProcessor = new Mock<IGeocodeServiceRequestProcessor>();
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_CountryConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new AddressServiceRequestProcessor(null, _addressVerificationDataProvider.Object,
                        _addressAutocompleteDataProvider.Object, _geocodeServiceRequestProcessor.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }

        [TestMethod]
        public void Constructor_AddressVerificationDataProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new AddressServiceRequestProcessor(_configurationProvider.Object, null,
                        _addressAutocompleteDataProvider.Object, _geocodeServiceRequestProcessor.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "addressVerificationDataProvider");
        }

        [TestMethod]
        public void Constructor_AddressAutocompleteDataProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new AddressServiceRequestProcessor(_configurationProvider.Object, _addressVerificationDataProvider.Object,
                        null, _geocodeServiceRequestProcessor.Object), SubSystemError.GeneralRequiredParameterIsUndefined, 
                        "addressAutocompleteDataProvider");
        }

        [TestMethod]
        public void Constructor_GeocodeServiceRequestProcessorIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new AddressServiceRequestProcessor(_configurationProvider.Object, _addressVerificationDataProvider.Object,
                        _addressAutocompleteDataProvider.Object, null), SubSystemError.GeneralRequiredParameterIsUndefined,
                        "geocodeServiceRequestProcessor");
        }

        #endregion

        #region ValidateAddress methods

        [TestMethod]
        public void ValidateAddress_SameGeocodingProvider_HappyPath()
        {
            var addressModel = GetAddressModel();

            _configurationProvider.Setup(p=>p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(TestModelsProvider.GetUsCountryConfiguration());
            _addressVerificationDataProvider.Setup(
                p => p.Verify(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(),
                    It.IsAny<bool>())).Returns(GetValidateAddressResponseModel());

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object, 
                _geocodeServiceRequestProcessor.Object).ValidateAddress(addressModel, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Bing, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateAddress_DifferentGeocodingProvider_HappyPath()
        {
            var addressModel = GetAddressModel();
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration(true);
            var validValidateAddressResponseModel = GetValidateAddressResponseModel(false);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(), It.IsAny<bool>())).Returns(validValidateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(addressModel, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Google, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateAddress_NoProvidersFound_ExceptionRaised()
        {
            var addressModel = GetAddressModel();
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetCountryConfigurationWithoutProviders();
            var validValidateAddressResponseModel = GetValidateAddressResponseModel(false);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(), It.IsAny<bool>())).Returns(validValidateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            ExceptionHelper.CheckException(
                () => new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(addressModel, LevelOfConfidence.High),
                SubSystemError.GeolocationDataProviderNotFound, DataProviderServiceType.AddressVerification);
        }

        [TestMethod]
        public void ValidateAddress_GeocodeResponseIsInvalid_EmptyLocationReturnedAndGeocodeProviderIsUndefined()
        {
            var addressModel = GetAddressModel();
            var geocodeResponseModel = GetInvalidGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration(true);
            var validValidateAddressResponseModel = GetValidateAddressResponseModel(false);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(), It.IsAny<bool>())).Returns(validValidateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(addressModel, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Undefined, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateAddress_FirstProviderAddressVerificaitonResponseIsInvalid_SecondProviderUsed()
        {
            var addressModel = GetAddressModel();
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();
            var invalidValidateAddressResponseModel = GetValidateAddressResponseModel(true, true);
            var validateAddressResponseModel = GetValidateAddressResponseModel(addressValidationProvider: DataProviderType.Google);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(addressModel, DataProviderType.Bing, LevelOfConfidence.High, true)).Returns(invalidValidateAddressResponseModel);
            _addressVerificationDataProvider.Setup(p => p.Verify(addressModel, DataProviderType.Google, LevelOfConfidence.High, true)).Returns(validateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(addressModel, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Bing, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateAddress_AllProviderAddressVerificaitonResponseIsInvalid_NoAddressOrLocationPopulated()
        {
            var addressModel = GetAddressModel();
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();
            var invalidValidateAddressResponseModel1 = GetValidateAddressResponseModel(true, true);
            var invalidValidateAddressResponseModel2 = GetValidateAddressResponseModel(true, true, DataProviderType.Google);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(addressModel, DataProviderType.Bing, LevelOfConfidence.High, true)).Returns(invalidValidateAddressResponseModel1);
            _addressVerificationDataProvider.Setup(p => p.Verify(addressModel, DataProviderType.Google, LevelOfConfidence.High, true)).Returns(invalidValidateAddressResponseModel2);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(addressModel, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(!result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Bing, result.GeocodingProvider);
        }

        #endregion

        #region ValidateFormattedAddress methods

        [TestMethod]
        public void ValidateFormattedAddress_SameGeocodingProvider_HappyPath()
        {
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(TestModelsProvider.GetUsCountryConfiguration());
            _addressVerificationDataProvider.Setup(p => p.Verify(It.IsAny<string>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(),
                    It.IsAny<bool>())).Returns(GetValidateAddressResponseModel());

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Bing, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateFormattedAddress_DifferentGeocodingProvider_HappyPath()
        {
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration(true);
            var validValidateAddressResponseModel = GetValidateAddressResponseModel(false);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(It.IsAny<string>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(), 
                It.IsAny<bool>())).Returns(validValidateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Google, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateFormattedAddress_NoProvidersFound_ExceptionRaised()
        {
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetCountryConfigurationWithoutProviders();
            var validValidateAddressResponseModel = GetValidateAddressResponseModel(false);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(), It.IsAny<bool>())).Returns(validValidateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            ExceptionHelper.CheckException(
                () => new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(FormattedAddress, Country, LevelOfConfidence.High),
                SubSystemError.GeolocationDataProviderNotFound, DataProviderServiceType.AddressVerification);
        }

        [TestMethod]
        public void ValidateFormattedAddress_GeocodeResponseIsInvalid_EmptyLocationReturnedAndGeocodeProviderIsUndefined()
        {
            var geocodeResponseModel = GetInvalidGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration(true);
            var validValidateAddressResponseModel = GetValidateAddressResponseModel(false);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(It.IsAny<string>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>(), It.IsAny<bool>()))
                .Returns(validValidateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Undefined, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateFormattedAddress_FirstProviderAddressVerificaitonResponseIsInvalid_SecondProviderUsed()
        {
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();
            var invalidValidateAddressResponseModel = GetValidateAddressResponseModel(true, true);
            var validateAddressResponseModel = GetValidateAddressResponseModel(addressValidationProvider: DataProviderType.Google);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(FormattedAddress, DataProviderType.Bing, LevelOfConfidence.High, true)).Returns(invalidValidateAddressResponseModel);
            _addressVerificationDataProvider.Setup(p => p.Verify(FormattedAddress, DataProviderType.Google, LevelOfConfidence.High, true)).Returns(validateAddressResponseModel);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Bing, result.GeocodingProvider);
        }

        [TestMethod]
        public void ValidateFormattedAddress_AllProviderAddressVerificaitonResponseIsInvalid_NoAddressOrLocationPopulated()
        {
            var geocodeResponseModel = GetGeocodeResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();
            var invalidValidateAddressResponseModel1 = GetValidateAddressResponseModel(true, true);
            var invalidValidateAddressResponseModel2 = GetValidateAddressResponseModel(true, true, DataProviderType.Google);

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _addressVerificationDataProvider.Setup(p => p.Verify(FormattedAddress, DataProviderType.Bing, LevelOfConfidence.High, true)).Returns(invalidValidateAddressResponseModel1);
            _addressVerificationDataProvider.Setup(p => p.Verify(FormattedAddress, DataProviderType.Google, LevelOfConfidence.High, true)).Returns(invalidValidateAddressResponseModel2);
            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>())).Returns(geocodeResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).ValidateAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(!result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Bing, result.GeocodingProvider);
        }

        #endregion

        #region AutocompleteAddressResponseModel tests

        [TestMethod]
        public void GetAutocompleteList_HappyPath()
        {
            var autocompleteResponseModel = GetAutocompleteAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteDefaultNumberOfHints)).Returns(15);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteMaximumNumberOfHints)).Returns(20);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);

            _addressAutocompleteDataProvider.Setup(p => p.GetAddressHintList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                        It.IsAny<DataProviderType>())).Returns(autocompleteResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).GetAutocompleteList(Country, AdministrativeArea, FormattedAddress, 10);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Addresses);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void GetAutocompleteList_MaxRecordsIsZero_DefaultNumberUsed()
        {
            var autocompleteResponseModel = GetAutocompleteAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteDefaultNumberOfHints)).Returns(15);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteMaximumNumberOfHints)).Returns(20);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);

            _addressAutocompleteDataProvider.Setup(p => p.GetAddressHintList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                        It.IsAny<DataProviderType>())).Returns(autocompleteResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).GetAutocompleteList(Country, AdministrativeArea, FormattedAddress, 0);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Addresses);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void GetAutocompleteList_MaxRecordsModeThanUpperLimit_DefaultNumberUsed()
        {
            var autocompleteResponseModel = GetAutocompleteAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteDefaultNumberOfHints)).Returns(15);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteMaximumNumberOfHints)).Returns(20);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);

            _addressAutocompleteDataProvider.Setup(p => p.GetAddressHintList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                        It.IsAny<DataProviderType>())).Returns(autocompleteResponseModel);

            var result = new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).GetAutocompleteList(Country, AdministrativeArea, FormattedAddress, 100);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Addresses);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void GetAutocompleteList_NoProviderFound_ExceptionRaised()
        {
            var autocompleteResponseModel = GetAutocompleteAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetCountryConfigurationWithoutProviders();

            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteDefaultNumberOfHints)).Returns(15);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteMaximumNumberOfHints)).Returns(20);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);

            _addressAutocompleteDataProvider.Setup(p => p.GetAddressHintList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                        It.IsAny<DataProviderType>())).Returns(autocompleteResponseModel);

            ExceptionHelper.CheckException(
                () => new AddressServiceRequestProcessor(_configurationProvider.Object,
                _addressVerificationDataProvider.Object, _addressAutocompleteDataProvider.Object,
                _geocodeServiceRequestProcessor.Object).GetAutocompleteList(Country, AdministrativeArea, FormattedAddress, 10),
                SubSystemError.GeolocationDataProviderNotFound, DataProviderServiceType.AddressAutoComplete);
        }

        #endregion

        #region Private methods

        private static AutocompleteAddressResponseModel GetAutocompleteAddressResponseModel()
        {
            return new AutocompleteAddressResponseModel
            {
                DataProvider = DataProviderType.Bing,
                IsValid = true,
                Addresses = new List<AddressModel>()
            };
        }

        private static GeocodeAddressResponseModel GetGeocodeResponseModel()
        {
            return new GeocodeAddressResponseModel
            {
                Address = GetAddressModel(),
                Confidence = LevelOfConfidence.High,
                DataProvider = DataProviderType.Google,
                IsValid = true,
                Location = GetLocationModel()
            };
        }

        private static GeocodeAddressResponseModel GetInvalidGeocodeResponseModel()
        {
            return new GeocodeAddressResponseModel
            {
                Address = GetAddressModel(),
                Confidence = LevelOfConfidence.High,
                DataProvider = DataProviderType.Google,
                IsValid = false,
                Location = GetLocationModel()
            };
        }

        private static ValidateAddressResponseModel GetValidateAddressResponseModel(bool includeLocation = true, bool isInvalid = false, DataProviderType addressValidationProvider = DataProviderType.Undefined)
        {
            return new ValidateAddressResponseModel
            {
                Address = GetAddressModel(),
                AddressValidationProvider = addressValidationProvider == DataProviderType.Undefined ? DataProviderType.Bing : addressValidationProvider,
                Confidence = LevelOfConfidence.High,
                GeocodingProvider = DataProviderType.Bing,
                IsValid = !isInvalid,
                Location = includeLocation ? GetLocationModel() : null
            };
        }
        private static LocationModel GetLocationModel()
        {
            return new LocationModel
            {
                Latitude = (decimal)33.755802,
                Longitude = (decimal)-118.308556
            };
        }

        private static AddressModel GetAddressModel()
        {
            return new AddressModel
            {
                Address1 = Address1,
                Address2 = Address2,
                AdministrativeArea = AdministrativeArea,
                Country = Country,
                City = City,
                FormattedAddress = FormattedAddress,
                PostalCode = PostalCode
            };
        }

        #endregion
    }
}
