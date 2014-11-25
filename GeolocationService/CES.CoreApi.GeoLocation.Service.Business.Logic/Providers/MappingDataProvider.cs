using System.Collections.Generic;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class MappingDataProvider : IMappingDataProvider
    {
        #region Core

        private readonly IEntityFactory _entityFactory;
        private readonly IDataResponseProvider _responseProvider;

        public MappingDataProvider(IEntityFactory entityFactory, IDataResponseProvider responseProvider)
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

        public GetMapResponseModel GetMap(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, 
            ICollection<PushPinModel> pushPins, DataProviderType providerType)
        {
            //Build data provider URL
            var urlBuilder = _entityFactory.GetInstance<IUrlBuilder>(providerType, FactoryEntity.UrlBuilder);
            var url = urlBuilder.BuildUrl(center, size, outputParameters, pushPins);

            //Get raw response from data provider
            var rawResponse = _responseProvider.GetBinaryResponse(url, providerType);

            //Parse raw response
            var parser = _entityFactory.GetInstance<IResponseParser>(providerType, FactoryEntity.Parser);
            var responseModel = parser.Parse(rawResponse);

            return responseModel;
        }

        #endregion
    }
}
