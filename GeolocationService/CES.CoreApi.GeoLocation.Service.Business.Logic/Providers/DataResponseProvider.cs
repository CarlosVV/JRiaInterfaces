﻿using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class DataResponseProvider : IDataResponseProvider
    {
        #region Core

        private readonly IHttpClientProxy _httpClientProxy;
        private readonly ILogManager _logManager;
        private readonly IRequestHeadersProvider _headerProvider;

        public DataResponseProvider(IHttpClientProxy httpClientProxy, ILogManager logManager, IRequestHeadersProvider headerProvider)
        {
            if (httpClientProxy == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "httpClientProxy");
            if (logManager == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "logManager");
            if (headerProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "headerProvider");

            _httpClientProxy = httpClientProxy;
            _logManager = logManager;
            _headerProvider = headerProvider;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Provides response from data provider
        /// </summary>
        /// <param name="requestUrl">Request URL</param>
        /// <param name="providerType">Data provider type - used for loggin purposes only</param>
        /// <returns></returns>
        public DataResponse GetResponse(string requestUrl, DataProviderType providerType)
        {
            if (string.IsNullOrEmpty(requestUrl))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralRequiredParameterIsUndefined, "requestUrl");

            //Get trace monitor instance
            var monitor = GetTraceLogMonitor(requestUrl, providerType);

            using (var httpClient = _httpClientProxy.GetHttpClient())
            {
                //Start tracing
                monitor.Start();

                var responseMessage = httpClient.GetAsync(requestUrl).Result;

                var dataResponse = new DataResponse(
                    responseMessage.Content.ReadAsStringAsync().Result,
                    responseMessage.StatusCode,
                    responseMessage.IsSuccessStatusCode);

                //Stop tracing
                monitor.DataContainer.ResponseMessage = dataResponse.RawResponse;
                monitor.Stop();

                return dataResponse;
            }
        }

        public BinaryDataResponse GetBinaryResponse(string requestUrl, DataProviderType providerType)
        {
            if (string.IsNullOrEmpty(requestUrl))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralRequiredParameterIsUndefined, "requestUrl");

            //Get trace monitor instance
            var monitor = GetTraceLogMonitor(requestUrl, providerType);

            using (var httpClient = _httpClientProxy.GetHttpClient())
            {
                //Start tracing
                monitor.Start();

                var responseMessage = httpClient.GetAsync(requestUrl).Result;

                var dataResponse = new BinaryDataResponse(
                    responseMessage.Content.ReadAsByteArrayAsync().Result,
                    responseMessage.StatusCode,
                    responseMessage.IsSuccessStatusCode);

                //Stop tracing
                monitor.DataContainer.BinaryResponseMessageLength = dataResponse.BinaryResponse.LongLength;
                monitor.Stop();

                return dataResponse;
            }
        }

        #endregion

        #region private methods

        private ITraceLogMonitor GetTraceLogMonitor(string requestUrl, DataProviderType providerType)
        {
            var monitor = _logManager.GetMonitorInstance<ITraceLogMonitor>();
            monitor.DataContainer.RequestMessage = requestUrl;
            monitor.DataContainer.Headers = _headerProvider.GetHeaders();
            monitor.DataContainer.ProviderType = providerType.ToString();
            return monitor;
        }

        #endregion
    }
}