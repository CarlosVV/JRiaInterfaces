﻿using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service
{
    [ServiceBehavior(Namespace = Namespaces.GeolocationServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GeoLocationService : IAddressService, IGeocodeService, IMapService, IHealthMonitoringService, IClientSideSupportService
    {
        #region Core

        private readonly IAddressServiceRequestProcessor _addressServiceRequestProcessor;
        private readonly IGeocodeServiceRequestProcessor _geocodeServiceRequestProcessor;
        private readonly IMapServiceRequestProcessor _mapServiceRequestProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IClientSideSupportServiceProcessor _clientSideSupportServiceProcessor;
        private readonly IMappingHelper _mapper;
        private readonly IRequestValidator _validator;

        public GeoLocationService(IAddressServiceRequestProcessor addressServiceRequestProcessor,
            IGeocodeServiceRequestProcessor geocodeServiceRequestProcessor,
            IMapServiceRequestProcessor mapServiceRequestProcessor,
            IHealthMonitoringProcessor healthMonitoringProcessor,
            IClientSideSupportServiceProcessor clientSideSupportServiceProcessor,
            IMappingHelper mapper, IRequestValidator validator)
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
            if (clientSideSupportServiceProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "clientSideSupportServiceProcessor");
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
            _clientSideSupportServiceProcessor = clientSideSupportServiceProcessor;
            _mapper = mapper;
            _validator = validator;
        }

        #endregion

        #region IAddressService implementation

        public virtual ValidateAddressResponse ValidateAddress(ValidateAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _addressServiceRequestProcessor.ValidateAddress(
                _mapper.ConvertTo<AddressRequest, AddressModel>(request.Address),
                _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.ConvertToResponse<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
        }

        public virtual ValidateAddressResponse ValidateAddress(ValidateFormattedAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _addressServiceRequestProcessor.ValidateAddress(
                request.FormattedAddress,
                request.Country,
                _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.ConvertToResponse<ValidateAddressResponseModel, ValidateAddressResponse>(responseModel);
        }

        public virtual AutocompleteAddressResponse GetAutocompleteList(AutocompleteAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _addressServiceRequestProcessor.GetAutocompleteList(
                request.Country,
                request.AdministrativeArea,
                request.Address,
                request.MaxRecords,
                _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.ConvertToResponse<AutocompleteAddressResponseModel, AutocompleteAddressResponse>(responseModel);
        }

        #endregion

        #region IGeocodeService implementation

        public virtual GeocodeAddressResponse GeocodeAddress(GeocodeAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _geocodeServiceRequestProcessor.GeocodeAddress(
                _mapper.ConvertTo<AddressRequest, AddressModel>(request.Address),
                _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
        }

        public virtual GeocodeAddressResponse GeocodeAddress(GeocodeFormattedAddressRequest request)
        {
            _validator.Validate(request);

            var responseModel = _geocodeServiceRequestProcessor.GeocodeAddress(
                request.FormattedAddress,
                request.Country,
                _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
        }

        public virtual GeocodeAddressResponse ReverseGeocodePoint(ReverseGeocodePointRequest request)
        {
            _validator.Validate(request);

            var responseModel = _geocodeServiceRequestProcessor.ReverseGeocodePoint(
                _mapper.ConvertTo<Location, LocationModel>(request.Location),
                request.Country,
                _mapper.ConvertTo<Confidence, LevelOfConfidence>(request.MinimumConfidence));

            return _mapper.ConvertToResponse<GeocodeAddressResponseModel, GeocodeAddressResponse>(responseModel);
        }

        #endregion

        #region IMapService implementation

        public virtual GetMapResponse GetMap(GetMapRequest request)
        {
            _validator.Validate(request);

            var responseModel = _mapServiceRequestProcessor.GetMap(
                request.Country,
                _mapper.ConvertTo<Location, LocationModel>(request.Center),
                _mapper.ConvertTo<MapSize, MapSizeModel>(request.MapSize),
                _mapper.ConvertTo<MapOutputParameters, MapOutputParametersModel>(request.MapOutputParameters),
                _mapper.ConvertTo<ICollection<PushPin>, ICollection<PushPinModel>>(request.PushPins));

            return _mapper.ConvertToResponse<GetMapResponseModel, GetMapResponse>(responseModel);
        }

        #endregion

        #region IHealthMonitoringService implementation

        public virtual ClearCacheResponse ClearCache()
        {
            var responseModel = _healthMonitoringProcessor.ClearCache();
            return _mapper.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public virtual HealthResponse Ping()
        {
            var responseModel = _healthMonitoringProcessor.Ping();
            return _mapper.ConvertToResponse<HealthResponseModel, HealthResponse>(responseModel);
        }

        #endregion

        #region IClientSideSupportService implementation

        public GetProviderKeyResponse GetProviderKey(GetProviderKeyRequest request)
        {
            _validator.Validate(request);

            var responseModel = _clientSideSupportServiceProcessor.GetProviderKey(
                _mapper.ConvertTo<DataProvider, DataProviderType>(request.DataProvider));

            return _mapper.ConvertToResponse<GetProviderKeyResponseModel, GetProviderKeyResponse>(responseModel);
        }

        public void LogEvent(LogEventRequest request)
        {
            _validator.Validate(request);

            _clientSideSupportServiceProcessor.LogEvent(
                _mapper.ConvertTo<DataProvider, DataProviderType>(request.DataProvider), request.Message);
        }

        #endregion
    }
}
