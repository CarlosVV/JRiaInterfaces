//using CES.CoreApi.Common.Enumerations;
//using CES.CoreApi.Common.Exceptions;

using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Interfaces;
using System.Net.Http;
namespace CES.CoreApi.GeoLocation.Logic.Providers
{
    public class DataResponseProvider : IDataResponseProvider
    {
   
        public DataResponse GetResponse(string requestUrl)
        {
			Logging.Log.Info(requestUrl);

			using (var httpClient = new HttpClient())
            {
                var responseMessage = httpClient.GetAsync(requestUrl).Result;

                var dataResponse = new DataResponse(
                    responseMessage.Content.ReadAsStringAsync().Result,
                    responseMessage.StatusCode,
                    responseMessage.IsSuccessStatusCode);
				Logging.Log.Info(dataResponse.RawResponse);
				return dataResponse;
            }
        }

        public BinaryDataResponse GetBinaryResponse(string requestUrl)
        {
		
            using (var httpClient = new HttpClient())
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