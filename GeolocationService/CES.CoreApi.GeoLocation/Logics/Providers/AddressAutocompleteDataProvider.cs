
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Interfaces;

namespace CES.CoreApi.GeoLocation.Logic.Providers
{
    public class AddressAutocompleteDataProvider : IAddressAutocompleteDataProvider
    {
        #region Core

        private readonly IUrlBuilderFactory _urlBuilderFactory;
        private readonly IResponseParserFactory _responseParserFactory;
        private readonly IDataResponseProvider _responseProvider;

        public AddressAutocompleteDataProvider(IUrlBuilderFactory urlBuilderFactory, IResponseParserFactory responseParserFactory,  IDataResponseProvider responseProvider)
        {          
            _urlBuilderFactory = urlBuilderFactory;
            _responseParserFactory = responseParserFactory;
            _responseProvider = responseProvider;
        }

        #endregion

        #region Public methods

        public AutocompleteAddressResponseModel GetAddressHintList(AutocompleteAddressModel address, int maxRecords, 
            DataProviderType providerType, LevelOfConfidence confidence)
        {
            //Build data provider URL
            var urlBuilder = _urlBuilderFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address, maxRecords);

            //Get raw response from data provider
            var rawResponse = _responseProvider.GetResponse(url);

            //Parse raw response
            var parser = _responseParserFactory.GetInstance<IResponseParser>(providerType);
            var responseModel = parser.ParseAutocompleteAddressResponse(rawResponse, maxRecords, confidence, address.Country);

            return responseModel;
        }

        #endregion
    }
}
