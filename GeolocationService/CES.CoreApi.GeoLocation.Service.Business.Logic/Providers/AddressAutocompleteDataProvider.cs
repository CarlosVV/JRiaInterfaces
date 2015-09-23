using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class AddressAutocompleteDataProvider : IAddressAutocompleteDataProvider
    {
        #region Core

        private readonly IUrlBuilderFactory _urlBuilderFactory;
        private readonly IResponseParserFactory _responseParserFactory;
        private readonly IDataResponseProvider _responseProvider;

        public AddressAutocompleteDataProvider(IUrlBuilderFactory urlBuilderFactory, IResponseParserFactory responseParserFactory,  IDataResponseProvider responseProvider)
        {
            if (urlBuilderFactory == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "urlBuilderFactory");
            if (responseProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
            if (responseParserFactory == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "responseParserFactory");
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
            var rawResponse = _responseProvider.GetResponse(url, providerType);

            //Parse raw response
            var parser = _responseParserFactory.GetInstance<IResponseParser>(providerType);
            var responseModel = parser.ParseAutocompleteAddressResponse(rawResponse, maxRecords, confidence, address.Country);

            return responseModel;
        }

        #endregion
    }
}
