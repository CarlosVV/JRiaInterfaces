using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Processors
{
    public class ClientSideSupportServiceProcessor : BaseServiceRequestProcessor, IClientSideSupportServiceProcessor
    {
        #region Core

        private readonly IHostApplicationProvider _hostApplicationProvider;
        private readonly ILogMonitorFactory _logMonitorFactory;

        public ClientSideSupportServiceProcessor(ICountryConfigurationProvider configurationProvider, IHostApplicationProvider hostApplicationProvider,
            ILogMonitorFactory logMonitorFactory)
            : base(configurationProvider)
        {
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");
            if (logMonitorFactory == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "logMonitorFactory");

            _hostApplicationProvider = hostApplicationProvider;
            _logMonitorFactory = logMonitorFactory;
        }

        #endregion

        #region Public methods

        public GetProviderKeyResponseModel GetProviderKey(DataProviderType providerType)
        {
            var hostApplication = _hostApplicationProvider.GetApplication();
            var configKey = GetLicenseKeyConfigurationKey(providerType);

            var foundKey = (from item in hostApplication.Result.Configuration
                where item.Name.Equals(configKey, StringComparison.OrdinalIgnoreCase)
                select item.Value)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(foundKey))
                return new GetProviderKeyResponseModel {IsValid = false};

            return new GetProviderKeyResponseModel
            {
                IsValid = true,
                ProviderKey = foundKey
            };
        }

        public void LogEvent(DataProviderType providerType, string message)
        {
            var traceMonitor = _logMonitorFactory.CreateNew<ITraceLogMonitor>();
            traceMonitor.DataContainer.ProviderType = providerType.ToString();
            traceMonitor.DataContainer.ClientSideMessage = message;
            traceMonitor.Start();
            traceMonitor.Stop();
        }

        #endregion

        #region Private methods

        private static string GetLicenseKeyConfigurationKey(DataProviderType providerType)
        {
            switch (providerType)
            {
                case DataProviderType.Bing:
                    return ConfigurationConstants.BingLicenseKeyConfigurationName_;
                case DataProviderType.Google:
                    return ConfigurationConstants.GoogleLicenseKeyConfigurationName;
                case DataProviderType.MelissaData:
                    return ConfigurationConstants.MelissaDataLicenseKeyConfigurationName;
                default:
                    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                        SubSystemError.GeneralInvalidParameterValue, "providerType", providerType);
            }
        }

        #endregion
    }
}
