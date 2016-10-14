using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Interfaces;


namespace CES.CoreApi.GeoLocation.Logic.Providers
{
    public class AddressVerificationDataProvider : IAddressVerificationDataProvider
    {
        #region Core

        private readonly IDataResponseProvider _responseProvider;
        private readonly IUrlBuilderFactory _urlBuilderFactory;
        private readonly IResponseParserFactory _responseParserFactory;

        public AddressVerificationDataProvider(IDataResponseProvider responseProvider, IUrlBuilderFactory urlBuilderFactory, IResponseParserFactory responseParserFactory)
        {
           
            _responseProvider = responseProvider;
            _urlBuilderFactory = urlBuilderFactory;
            _responseParserFactory = responseParserFactory;
        }

        #endregion

        #region Public methods

        public ValidateAddressResponseModel Verify(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _urlBuilderFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Verify(providerType, acceptableConfidence, url);
        }

        public ValidateAddressResponseModel Verify(string address, DataProviderType providerType, LevelOfConfidence acceptableConfidence)
        {
            //Build data provider URL
            var urlBuilder = _urlBuilderFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Verify(providerType, acceptableConfidence, url);
        }

        #endregion

        #region Private methods

        private ValidateAddressResponseModel Verify(DataProviderType providerType, LevelOfConfidence acceptableConfidence, string url)
        {
            //Get raw response from data provider
            var rawResponse = _responseProvider.GetResponse(url);

            //Parse raw response
            var parser = _responseParserFactory.GetInstance<IResponseParser>(providerType);
            var responseModel = parser.ParseValidateAddressResponse(rawResponse, acceptableConfidence);

            return responseModel;
        }

        #endregion
    }
}
