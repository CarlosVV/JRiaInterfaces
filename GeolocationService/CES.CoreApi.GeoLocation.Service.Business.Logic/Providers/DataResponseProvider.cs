using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class DataResponseProvider : IDataResponseProvider
    {
        private readonly IHttpClientProxy _httpClientProxy;

        public DataResponseProvider(IHttpClientProxy httpClientProxy)
        {
            if (httpClientProxy == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "httpClientProxy");
            
            _httpClientProxy = httpClientProxy;
        }

        public DataResponse GetResponse(string requestUrl, DataProviderType providerType)
        {
            if (string.IsNullOrEmpty(requestUrl))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralRequiredParameterIsUndefined, "requestUrl");

            using (var httpClient = _httpClientProxy.GetHttpClient())
            {
                var responseMessage = httpClient.GetAsync(requestUrl).Result;

                var dataResponse = new DataResponse(
                    responseMessage.Content.ReadAsStringAsync().Result,
                    responseMessage.StatusCode,
                    responseMessage.IsSuccessStatusCode);

                return dataResponse;
            }
        }

        public BinaryDataResponse GetBinaryResponse(string requestUrl, DataProviderType providerType)
        {
            if (string.IsNullOrEmpty(requestUrl))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralRequiredParameterIsUndefined, "requestUrl");
			
            using (var httpClient = _httpClientProxy.GetHttpClient())
            {
                var responseMessage = httpClient.GetAsync(requestUrl).Result;

                var dataResponse = new BinaryDataResponse(
                    responseMessage.Content.ReadAsByteArrayAsync().Result,
                    responseMessage.StatusCode,
                    responseMessage.IsSuccessStatusCode);

				return dataResponse;
            }
        }
    }
}