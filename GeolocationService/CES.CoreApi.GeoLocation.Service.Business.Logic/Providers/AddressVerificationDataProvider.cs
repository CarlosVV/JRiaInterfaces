using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class AddressVerificationDataProvider : IAddressVerificationDataProvider
    {
        #region Core

        private readonly IDataResponseProvider _responseProvider;
        private readonly IEntityFactory _entityFactory;

        public AddressVerificationDataProvider(IDataResponseProvider responseProvider, IEntityFactory entityFactory)
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

        public ValidateAddressResponseModel Verify(AddressModel address, DataProviderType providerType, LevelOfConfidence acceptableConfidence, bool includeLocation)
        {
            //Build data provider URL
            var urlBuilder = _entityFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Verify(providerType, acceptableConfidence, includeLocation, url);
        }

        public ValidateAddressResponseModel Verify(string address, DataProviderType providerType,
            LevelOfConfidence acceptableConfidence, bool includeLocation)
        {
            //Build data provider URL
            var urlBuilder = _entityFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address);

            return Verify(providerType, acceptableConfidence, includeLocation, url);
        }

        #endregion

        #region Private methods

        private ValidateAddressResponseModel Verify(DataProviderType providerType, LevelOfConfidence acceptableConfidence, bool includeLocation, string url)
        {
            //Get raw response from data provider
            var rawResponse = _responseProvider.GetResponse(url, providerType);

            //Parse raw response
            var parser = _entityFactory.GetInstance<IResponseParser>(providerType, FactoryEntity.Parser);
            var responseModel = parser.Parse(rawResponse, acceptableConfidence, includeLocation);

            return responseModel;
        }

        #endregion
    }
}
