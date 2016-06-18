using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Proxies;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class DataResponseProvider : IDataResponseProvider
    {
       // private readonly IHttpClientProxy2 _httpClientProxy;

        public DataResponseProvider()
        {          
            
       
        }

        public DataResponse GetResponse(string requestUrl, DataProviderType providerType)
        {
            if (string.IsNullOrEmpty(requestUrl))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralRequiredParameterIsUndefined, "requestUrl");

            using (var httpClient = HttpClientProxy.GetHttpClient())
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
			
            using (var httpClient = HttpClientProxy.GetHttpClient())
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