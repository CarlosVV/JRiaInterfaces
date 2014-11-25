using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class AddressAutocompleteDataProvider : IAddressAutocompleteDataProvider
    {
        #region Core

        private readonly IEntityFactory _entityFactory;
        private readonly IDataResponseProvider _responseProvider;

        public AddressAutocompleteDataProvider(IEntityFactory entityFactory, IDataResponseProvider responseProvider)
        {
            if (entityFactory == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "entityFactory");
            if (responseProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
            _entityFactory = entityFactory;
            _responseProvider = responseProvider;
        }

        #endregion

        #region Public methods

        public AutocompleteAddressResponseModel GetAddressHintList(string country, string administrativeArea, string address, int maxRecords, DataProviderType providerType)
        {
            //Build data provider URL
            var urlBuilder = _entityFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(address, administrativeArea, country, maxRecords);

            //Get raw response from data provider
            var rawResponse = _responseProvider.GetResponse(url, providerType);

            //Parse raw response
            var parser = _entityFactory.GetInstance<IResponseParser>(providerType, FactoryEntity.Parser);
            var responseModel = parser.Parse(rawResponse, maxRecords, country);

            return responseModel;
        }

        #endregion
    }
}
