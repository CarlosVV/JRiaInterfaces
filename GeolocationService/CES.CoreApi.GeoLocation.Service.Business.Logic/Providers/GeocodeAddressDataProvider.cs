using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class GeocodeAddressDataProvider : IGeocodeAddressDataProvider
    {
         #region Core

        private readonly IDataResponseProvider _responseProvider;
        private readonly IUrlBuilderFactory _urlBuilderFactory;
        private readonly IResponseParserFactory _responseParserFactory;

        public GeocodeAddressDataProvider(IDataResponseProvider responseProvider, IUrlBuilderFactory urlBuilderFactory, IResponseParserFactory responseParserFactory)
        {
            if (responseProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
            if (urlBuilderFactory == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "urlBuilderFactory");
            if (responseParserFactory == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "responseParserFactory");

            _responseProvider = responseProvider;
            _urlBuilderFactory = urlBuilderFactory;
            _responseParserFactory = responseParserFactory;
        }

        #endregion

        #region Public methods

        public GeocodeAddressResponseModel Geocode(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _urlBuilderFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Process(providerType, acceptableConfidence, url);
        }

        public GeocodeAddressResponseModel Geocode(string address, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _urlBuilderFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Process(providerType, acceptableConfidence, url);
        }

        public GeocodeAddressResponseModel ReverseGeocode(LocationModel location, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _urlBuilderFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
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
            var parser = _responseParserFactory.GetInstance<IResponseParser>(providerType);
            var responseModel = parser.ParseGeocodeAddressResponse(rawResponse, acceptableConfidence);

            return responseModel;
        }

        #endregion
    }
}
