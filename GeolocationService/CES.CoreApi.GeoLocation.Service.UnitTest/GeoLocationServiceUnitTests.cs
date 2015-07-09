using System.Collections.Generic;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.UnitTest
{
    [TestClass]
    public class GeoLocationServiceUnitTests
    {
        private Mock<IAddressServiceRequestProcessor> _addressServiceRequestProcessor;
        private Mock<IGeocodeServiceRequestProcessor> _geocodeServiceRequestProcessor;
        private Mock<IMapServiceRequestProcessor> _mapServiceRequestProcessor;
        private Mock<IHealthMonitoringProcessor> _healthMonitoringProcessor;
        private Mock<IClientSideSupportServiceProcessor> _clientSideSupportServiceProcessor;
        private Mock<IRequestValidator> _requestValidator;
        private Mock<IMappingHelper> _mappingHelper;
        private GeoLocationService _geoLocationService;

        [TestInitialize]
        public void Setup()
        {
            _addressServiceRequestProcessor = new Mock<IAddressServiceRequestProcessor>();
            _geocodeServiceRequestProcessor = new Mock<IGeocodeServiceRequestProcessor>();
            _mapServiceRequestProcessor = new Mock<IMapServiceRequestProcessor>();
            _healthMonitoringProcessor = new Mock<IHealthMonitoringProcessor>();
            _clientSideSupportServiceProcessor = new Mock<IClientSideSupportServiceProcessor>();
            _requestValidator = new Mock<IRequestValidator>();
            _mappingHelper = new Mock<IMappingHelper>();

            _geoLocationService = new GeoLocationService(_addressServiceRequestProcessor.Object,
                _geocodeServiceRequestProcessor.Object, _mapServiceRequestProcessor.Object,
                _healthMonitoringProcessor.Object, _clientSideSupportServiceProcessor.Object, 
                _mappingHelper.Object, _requestValidator.Object);
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_AddressServiceRequestProcessorIsNull_RaiseException()
        {
            ExceptionHelper.CheckException(() => new GeoLocationService(null, _geocodeServiceRequestProcessor.Object, _mapServiceRequestProcessor.Object,
                _healthMonitoringProcessor.Object, _clientSideSupportServiceProcessor.Object, _mappingHelper.Object, _requestValidator.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "addressServiceRequestProcessor");
        }

        [TestMethod]
        public void Constructor_GeocodeServiceRequestProcessorIsNull_RaiseException()
        {
            ExceptionHelper.CheckException(() => new GeoLocationService(_addressServiceRequestProcessor.Object, null, _mapServiceRequestProcessor.Object,
                _healthMonitoringProcessor.Object, _clientSideSupportServiceProcessor.Object, _mappingHelper.Object, _requestValidator.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "geocodeServiceRequestProcessor");
        }

        [TestMethod]
        public void Constructor_MapServiceRequestProcessorIsNull_RaiseException()
        {
            ExceptionHelper.CheckException(() => new GeoLocationService(_addressServiceRequestProcessor.Object, _geocodeServiceRequestProcessor.Object, null,
                _healthMonitoringProcessor.Object, _clientSideSupportServiceProcessor.Object, _mappingHelper.Object, _requestValidator.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "mapServiceRequestProcessor");
        }

        [TestMethod]
        public void Constructor_HealthMonitoringProcessorIsNull_RaiseException()
        {
            ExceptionHelper.CheckException(() => new GeoLocationService(_addressServiceRequestProcessor.Object, _geocodeServiceRequestProcessor.Object,
                    _mapServiceRequestProcessor.Object, null, _clientSideSupportServiceProcessor.Object, _mappingHelper.Object, _requestValidator.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
        }

        [TestMethod]
        public void Constructor_ClientSideSupportServiceProcessorIsNull_RaiseException()
        {
            ExceptionHelper.CheckException(() => new GeoLocationService(_addressServiceRequestProcessor.Object, _geocodeServiceRequestProcessor.Object,
                    _mapServiceRequestProcessor.Object, _healthMonitoringProcessor.Object, null, _mappingHelper.Object, _requestValidator.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "clientSideSupportServiceProcessor");
        }

        [TestMethod]
        public void Constructor_MappingHelperIsNull_RaiseException()
        {
            ExceptionHelper.CheckException(() => new GeoLocationService(_addressServiceRequestProcessor.Object, _geocodeServiceRequestProcessor.Object,
                    _mapServiceRequestProcessor.Object, _healthMonitoringProcessor.Object, _clientSideSupportServiceProcessor.Object, null, _requestValidator.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
        }

        [TestMethod]
        public void Constructor_RequestValidatorIsNull_RaiseException()
        {
            ExceptionHelper.CheckException(() => new GeoLocationService(_addressServiceRequestProcessor.Object, _geocodeServiceRequestProcessor.Object,
                    _mapServiceRequestProcessor.Object, _healthMonitoringProcessor.Object, _clientSideSupportServiceProcessor.Object, _mappingHelper.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "validator");
        }

        [TestMethod]
        public void Constructor_SuccessPath_NoExceptionRaised()
        {
            ExceptionHelper.CheckHappyPath(() => new GeoLocationService(_addressServiceRequestProcessor.Object, _geocodeServiceRequestProcessor.Object,
                    _mapServiceRequestProcessor.Object, _healthMonitoringProcessor.Object, _clientSideSupportServiceProcessor.Object, _mappingHelper.Object, 
                    _requestValidator.Object));
        }

        #endregion

        [TestMethod]
        public void ValidateAddress_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            var request = new ValidateAddressRequest();
            
            _requestValidator.Setup(p => p.Validate(request)).Verifiable();
           
            _addressServiceRequestProcessor.Setup(p => p.ValidateAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>()))
                .Returns(It.IsAny<ValidateAddressResponseModel>)
                .Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<ValidateAddressResponseModel, ValidateAddressResponse>(It.IsAny<ValidateAddressResponseModel>()))
                .Returns(It.IsAny<ValidateAddressResponse>())
                .Verifiable();

            _geoLocationService.ValidateAddress(request);

            _requestValidator.Verify(p => p.Validate(It.IsAny<ValidateAddressRequest>()), Times.Exactly(1));
            _addressServiceRequestProcessor.Verify(p => p.ValidateAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>()), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<ValidateAddressResponseModel, ValidateAddressResponse>(It.IsAny<ValidateAddressResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ValidateFormattedAddress_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            var request = new ValidateFormattedAddressRequest();

            _requestValidator.Setup(p => p.Validate(request)).Verifiable();

            _addressServiceRequestProcessor.Setup(p => p.ValidateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>()))
                .Returns(It.IsAny<ValidateAddressResponseModel>)
                .Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<ValidateAddressResponseModel, ValidateAddressResponse>(It.IsAny<ValidateAddressResponseModel>()))
                .Returns(It.IsAny<ValidateAddressResponse>())
                .Verifiable();

            _geoLocationService.ValidateAddress(request);

            _requestValidator.Verify(p => p.Validate(It.IsAny<ValidateFormattedAddressRequest>()), Times.Exactly(1));
            _addressServiceRequestProcessor.Verify(p => p.ValidateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>()), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<ValidateAddressResponseModel, ValidateAddressResponse>(It.IsAny<ValidateAddressResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void GetAutocompleteList_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            var request = new AutocompleteAddressRequest();

            _requestValidator.Setup(p => p.Validate(request)).Verifiable();

            _addressServiceRequestProcessor.Setup(p => p.GetAutocompleteList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LevelOfConfidence>()))
                .Returns(It.IsAny<AutocompleteAddressResponseModel>)
                .Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(It.IsAny<AutocompleteAddressResponseModel>()))
                .Returns(It.IsAny<AutocompleteAddressResponse>())
                .Verifiable();

            _geoLocationService.GetAutocompleteList(request);

            _requestValidator.Verify(p => p.Validate(It.IsAny<AutocompleteAddressRequest>()), Times.Exactly(1));
            _addressServiceRequestProcessor.Verify(p => p.GetAutocompleteList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<LevelOfConfidence>()), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(It.IsAny<AutocompleteAddressResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void GeocodeAddress_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            var request = new GeocodeAddressRequest();

            _requestValidator.Setup(p => p.Validate(request)).Verifiable();

            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>()))
                .Returns(It.IsAny<GeocodeAddressResponseModel>)
                .Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(It.IsAny<GeocodeAddressResponseModel>()))
                .Returns(It.IsAny<GeocodeAddressResponse>())
                .Verifiable();

            _geoLocationService.GeocodeAddress(request);

            _requestValidator.Verify(p => p.Validate(It.IsAny<GeocodeAddressRequest>()), Times.Exactly(1));
            _geocodeServiceRequestProcessor.Verify(p => p.GeocodeAddress(It.IsAny<AddressModel>(), It.IsAny<LevelOfConfidence>()), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(It.IsAny<GeocodeAddressResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void GeocodeFormattedAddress_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            var request = new GeocodeFormattedAddressRequest();

            _requestValidator.Setup(p => p.Validate(request)).Verifiable();

            _geocodeServiceRequestProcessor.Setup(p => p.GeocodeAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>()))
                .Returns(It.IsAny<GeocodeAddressResponseModel>)
                .Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(It.IsAny<GeocodeAddressResponseModel>()))
                 .Returns(It.IsAny<GeocodeAddressResponse>())
                 .Verifiable();

            _geoLocationService.GeocodeAddress(request);

            _requestValidator.Verify(p => p.Validate(It.IsAny<GeocodeFormattedAddressRequest>()), Times.Exactly(1));
            _geocodeServiceRequestProcessor.Verify(p => p.GeocodeAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>()), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(It.IsAny<GeocodeAddressResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ReverseGeocodePoint_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            var request = new ReverseGeocodePointRequest();
         
            _requestValidator.Setup(p => p.Validate(request)).Verifiable();

            _geocodeServiceRequestProcessor.Setup(p => p.ReverseGeocodePoint(It.IsAny<LocationModel>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>()))
                .Returns(It.IsAny<GeocodeAddressResponseModel>)
                .Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(It.IsAny<GeocodeAddressResponseModel>()))
                 .Returns(It.IsAny<GeocodeAddressResponse>())
                 .Verifiable();

            _geoLocationService.ReverseGeocodePoint(request);

            _requestValidator.Verify(p => p.Validate(It.IsAny<ReverseGeocodePointRequest>()), Times.Exactly(1));
            _geocodeServiceRequestProcessor.Verify(p => p.ReverseGeocodePoint(It.IsAny<LocationModel>(), It.IsAny<string>(), It.IsAny<LevelOfConfidence>()), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(It.IsAny<GeocodeAddressResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void GetMap_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            var request = new GetMapRequest();

            _requestValidator.Setup(p => p.Validate(request)).Verifiable();

            _mapServiceRequestProcessor.Setup(
                p => p.GetMap(It.IsAny<string>(), It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(),
                    It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>()))
                .Returns(It.IsAny<GetMapResponseModel>)
                .Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<GetMapResponseModel, GetMapResponse>(It.IsAny<GetMapResponseModel>()))
                 .Returns(It.IsAny<GetMapResponse>())
                 .Verifiable();

            _geoLocationService.GetMap(request);

            _requestValidator.Verify(p => p.Validate(It.IsAny<GetMapRequest>()), Times.Exactly(1));
            _mapServiceRequestProcessor.Verify(
                p => p.GetMap(It.IsAny<string>(), It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(),
                    It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>()), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<GetMapResponseModel, GetMapResponse>(It.IsAny<GetMapResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ClearCache_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            _healthMonitoringProcessor.Setup(p => p.ClearCache()).Returns(It.IsAny<ClearCacheResponseModel>).Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(It.IsAny<ClearCacheResponseModel>()))
                 .Returns(It.IsAny<ClearCacheResponse>())
                 .Verifiable();

            _geoLocationService.ClearCache();

            _healthMonitoringProcessor.Verify(p => p.ClearCache(), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(It.IsAny<ClearCacheResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void Ping_SuccessPath_AllDependenciesCalledNoExceptionRaised()
        {
            _healthMonitoringProcessor.Setup(p => p.Ping()).Returns(It.IsAny<PingResponseModel>).Verifiable();

            _mappingHelper.Setup(p => p.ConvertToResponse<PingResponseModel, PingResponse>(It.IsAny<PingResponseModel>()))
                 .Returns(It.IsAny<PingResponse>())
                 .Verifiable();

            _geoLocationService.Ping();

            _healthMonitoringProcessor.Verify(p => p.Ping(), Times.Exactly(1));
            _mappingHelper.Verify(p => p.ConvertToResponse<PingResponseModel, PingResponse>(It.IsAny<PingResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void GetProviderKey_SuccessPath_NoExceptionRaised()
        {
            _requestValidator.Setup(p => p.Validate(It.IsAny<GetProviderKeyRequest>())).Verifiable();
            
            _clientSideSupportServiceProcessor.Setup(p => p.GetProviderKey(It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<GetProviderKeyResponseModel>)
                .Verifiable();
            
            _mappingHelper.Setup(p => p.ConvertToResponse<GetProviderKeyResponseModel, GetProviderKeyResponse>(It.IsAny<GetProviderKeyResponseModel>()))
                 .Returns(It.IsAny<GetProviderKeyResponse>())
                 .Verifiable();

            _geoLocationService.GetProviderKey(new GetProviderKeyRequest());

            _requestValidator.Verify(p => p.Validate(It.IsAny<GetProviderKeyRequest>()), Times.Exactly(1));

             _clientSideSupportServiceProcessor.Verify(p => p.GetProviderKey(It.IsAny<DataProviderType>()), Times.Exactly(1));
             _mappingHelper.Verify(p => p.ConvertToResponse<GetProviderKeyResponseModel, GetProviderKeyResponse>(It.IsAny<GetProviderKeyResponseModel>()), Times.Exactly(1));
        }

        [TestMethod]
        public void LogEvent_SuccessPath_NoExceptionRaised()
        {
            _requestValidator.Setup(p => p.Validate(It.IsAny<LogEventRequest>())).Verifiable();

            _clientSideSupportServiceProcessor.Setup(p => p.LogEvent(It.IsAny<DataProviderType>(), It.IsAny<string>())).Verifiable();

            _geoLocationService.LogEvent(new LogEventRequest());

            _requestValidator.Verify(p => p.Validate(It.IsAny<LogEventRequest>()), Times.Exactly(1));

            _clientSideSupportServiceProcessor.Verify(p => p.LogEvent(It.IsAny<DataProviderType>(), It.IsAny<string>()), Times.Exactly(1));
        }
    }
}
