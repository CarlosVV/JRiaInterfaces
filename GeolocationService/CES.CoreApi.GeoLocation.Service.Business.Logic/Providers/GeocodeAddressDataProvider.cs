using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class GeocodeAddressDataProvider : IGeocodeAddressDataProvider
    {
         #region Core

        private readonly IDataResponseProvider _responseProvider;
        private readonly IEntityFactory _entityFactory;

        public GeocodeAddressDataProvider(IDataResponseProvider responseProvider, IEntityFactory entityFactory)
        {
            if (responseProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
            if (entityFactory == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "entityFactory");

            _responseProvider = responseProvider;
            _entityFactory = entityFactory;
        }

        #endregion

        #region Public methods

        public GeocodeAddressResponseModel Geocode(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _entityFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Process(providerType, acceptableConfidence, url);
        }

        public GeocodeAddressResponseModel Geocode(string address, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _entityFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Process(providerType, acceptableConfidence, url);
        }

        public GeocodeAddressResponseModel ReverseGeocode(LocationModel location, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _entityFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(location);

            return Process(providerType, acceptableConfidence, url);
        }

        #endregion

        #region Private methods

        private GeocodeAddressResponseModel Process(DataProviderType providerType, LevelOfConfidence acceptableConfidence, string url)
        {
            //Get raw response from data provider
            var rawResponse = _responseProvider.GetResponse(url, providerType);

            //Parse raw response
            var parser = _entityFactory.GetInstance<IResponseParser>(providerType, FactoryEntity.Parser);
            var responseModel = parser.Parse(rawResponse, acceptableConfidence);

            return responseModel;
        }

        #endregion
    }
}
