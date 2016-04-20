using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Constants;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using AutoMapper;

namespace CES.CoreApi.GeoLocation.Service
{
	[ServiceBehavior(Namespace = Namespaces.GeolocationServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GeoLocationService : IAddressService, IGeocodeService, IMapService, IHealthMonitoringService
    {
        #region Core

        private readonly IAddressServiceRequestProcessor _addressServiceRequestProcessor;
        private readonly IGeocodeServiceRequestProcessor _geocodeServiceRequestProcessor;
        private readonly IMapServiceRequestProcessor _mapServiceRequestProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
  
        private readonly IMapper _mapper;
        private readonly IRequestValidator _validator;

        public GeoLocationService(IAddressServiceRequestProcessor addressServiceRequestProcessor,
            IGeocodeServiceRequestProcessor geocodeServiceRequestProcessor,
            IMapServiceRequestProcessor mapServiceRequestProcessor,
            IHealthMonitoringProcessor healthMonitoringProcessor,

			IMapper mapper, IRequestValidator validator)
        {
            if (addressServiceRequestProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "addressServiceRequestProcessor");
            if (geocodeServiceRequestProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "geocodeServiceRequestProcessor");
            if (mapServiceRequestProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapServiceRequestProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
          
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");
            if (validator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "validator");

            _addressServiceRequestProcessor = addressServiceRequestProcessor;
            _geocodeServiceRequestProcessor = geocodeServiceRequestProcessor;
            _mapServiceRequestProcessor = mapServiceRequestProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
        
            _mapper = mapper;
            _validator = validator;
        }

        #endregion

        #region IAddressService implementation

        public virtual ValidateAddressResponse ValidateAddress(ValidateAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _addressServiceRequestProcessor.ValidateAddress(
                _mapper.Map<AddressRequest, AddressModel>(request.Address),
                _mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.Map<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
        }

        public virtual ValidateAddressResponse ValidateAddress(ValidateFormattedAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _addressServiceRequestProcessor.ValidateAddress(
                request.FormattedAddress,
                request.Country,
                _mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.Map<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
        }

        public virtual AutocompleteAddressResponse GetAutocompleteList(AutocompleteAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _addressServiceRequestProcessor.GetAutocompleteList(
                _mapper.Map<AddressRequest, AutocompleteAddressModel>(request.Address),
                request.MaxRecords,
                _mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.Map<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);
        }

        #endregion

        #region IGeocodeService implementation

        public virtual GeocodeAddressResponse GeocodeAddress(GeocodeAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _geocodeServiceRequestProcessor.GeocodeAddress(
                _mapper.Map<AddressRequest, AddressModel>(request.Address),
                _mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.Map<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
        }

        public virtual GeocodeAddressResponse GeocodeAddress(GeocodeFormattedAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _geocodeServiceRequestProcessor.GeocodeAddress(
                request.FormattedAddress,
                request.Country,
                _mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.Map<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
        }

        public virtual GeocodeAddressResponse ReverseGeocodePoint(ReverseGeocodePointRequest request)
        {
            _validator.Validate(request);

            var responseModel = _geocodeServiceRequestProcessor.ReverseGeocodePoint(
                _mapper.Map<Location, LocationModel>(request.Location),
                request.Country,
                _mapper.Map<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.Map<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
        }

        #endregion

        #region IMapService implementation

        public virtual GetMapResponse GetMap(GetMapRequest request)
        {
            _validator.Validate(request);

            var responseModel = _mapServiceRequestProcessor.GetMap(
                request.Country,
                _mapper.Map<Location, LocationModel>(request.Center),
                _mapper.Map<MapSize, MapSizeModel>(request.MapSize),
                _mapper.Map<MapOutputParameters, MapOutputParametersModel>(request.MapOutputParameters),
                _mapper.Map<ICollection<PushPin>, ICollection<PushPinModel>>(request.PushPins));

            return _mapper.Map<GetMapResponseModel, GetMapResponse>(responseModel);
        }

        #endregion

        #region IHealthMonitoringService implementation

        public virtual ClearCacheResponse ClearCache()
        {
            var responseModel = _healthMonitoringProcessor.ClearCache();
            return _mapper.Map<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public virtual PingResponse Ping()
        {
            var responseModel = _healthMonitoringProcessor.Ping() as PingResponseModel;
            return _mapper.Map<PingResponseModel, PingResponse>(responseModel);
        }

        #endregion

        
    }
}
