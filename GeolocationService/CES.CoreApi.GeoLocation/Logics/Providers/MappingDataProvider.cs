using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Logic.Providers
{
    public class MappingDataProvider : IMappingDataProvider
    {
        #region Core

        private readonly IUrlBuilderFactory _urlBuilderFactory;
        private readonly IDataResponseProvider _responseProvider;
        private readonly IResponseParserFactory _responseParserFactory;

        public MappingDataProvider(IUrlBuilderFactory urlBuilderFactory, IDataResponseProvider responseProvider, IResponseParserFactory responseParserFactory)
        {
         
            _urlBuilderFactory = urlBuilderFactory;
            _responseProvider = responseProvider;
            _responseParserFactory = responseParserFactory;
        }

        #endregion

        #region Public methods

        public GetMapResponseModel GetMap(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, 
            ICollection<PushPinModel> pushPins, DataProviderType providerType)
        {
            //Build data provider URL
            var urlBuilder = _urlBuilderFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(center, size, outputParameters, pushPins);

            //Get raw response from data provider
            var rawResponse = _responseProvider.GetBinaryResponse(url);

            //Parse raw response
            var parser = _responseParserFactory.GetInstance<IResponseParser>(providerType);
            var responseModel = parser.ParseMapResponse(rawResponse);

            return responseModel;
        }

        #endregion
    }
}
