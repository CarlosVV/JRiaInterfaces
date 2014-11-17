using System;
using System.Linq;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
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
        private readonly ILogManager _logManager;

        public ClientSideSupportServiceProcessor(ICountryConfigurationProvider configurationProvider, IHostApplicationProvider hostApplicationProvider,
            ILogManager logManager)
            : base(configurationProvider)
        {
            if (hostApplicationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");
            if (logManager == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "logManager");

            _hostApplicationProvider = hostApplicationProvider;
            _logManager = logManager;
        }

        #endregion

        #region Public methods

        public GetProviderKeyResponseModel GetProviderKey(DataProviderType providerType)
        {
            var hostApplication = _hostApplicationProvider.GetApplication();
            var configKey = GetLicenseKeyConfigurationKey(providerType);

            var foundKey = (from item in hostApplication.Configuration
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
            var monitor = _logManager.GetMonitorInstance<ITraceLogMonitor>();
            monitor.DataContainer.ProviderType = providerType.ToString();
            monitor.DataContainer.ClientSideMessage = message;
            monitor.Start();
            monitor.Stop();
        }

        #endregion

        #region Private methods

        private static string GetLicenseKeyConfigurationKey(DataProviderType providerType)
        {
            switch (providerType)
            {
                case DataProviderType.Bing:
                    return ConfigurationConstants.BingLicenseKeyConfigurationName;
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
