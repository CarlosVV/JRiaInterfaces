using System;
using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Processors
{
    public class MapServiceRequestProcessor : BaseServiceRequestProcessor, IMapServiceRequestProcessor
    {
        #region Core

        private readonly IMappingDataProvider _mappingDataProvider;

        public MapServiceRequestProcessor(ICountryConfigurationProvider configurationProvider, IMappingDataProvider mappingDataProvider) 
            : base(configurationProvider)
        {
            if (mappingDataProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "mappingDataProvider");
            _mappingDataProvider = mappingDataProvider;
        }

        #endregion

        #region Public methods

        public GetMapResponseModel GetMap(string country, LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins)
        {
            var providerConfiguration = GetProviderConfigurationByCountry(country, DataProviderServiceType.Mapping).FirstOrDefault();

            if (providerConfiguration == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationDataProviderNotFound,
                    DataProviderServiceType.Mapping);

            //Set default image format if it is not defined
            if (outputParameters.ImageFormat == ImageFormat.Undefined)
                outputParameters.ImageFormat = ImageFormat.Jpeg;
            //Set default map style if it is not defined
            if (outputParameters.MapStyle == MapStyle.Undefined)
                outputParameters.MapStyle = MapStyle.Road;

            return _mappingDataProvider.GetMap(center, size, outputParameters, pushPins, providerConfiguration.DataProviderType);
        }

        #endregion
    }
}
