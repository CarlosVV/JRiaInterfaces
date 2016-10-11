using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Logic.Processors
{
    public class MapServiceRequestProcessor : BaseServiceRequestProcessor, IMapServiceRequestProcessor
    {
        #region Core

        private readonly IMappingDataProvider _mappingDataProvider;

        public MapServiceRequestProcessor(IMappingDataProvider mappingDataProvider)             
        {          
            _mappingDataProvider = mappingDataProvider;
        }

        #endregion

        #region Public methods

        public GetMapResponseModel GetMap(string country, LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins)
        {
            var providerConfiguration = GetProviderConfigurationByCountry(country, DataProviderServiceType.Mapping).FirstOrDefault();

            //if (providerConfiguration == null)
            //    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
            //        SubSystemError.GeolocationDataProviderNotFound,
            //        DataProviderServiceType.Mapping);

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
