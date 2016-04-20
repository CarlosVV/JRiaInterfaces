using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class CountryConfigurationProvider 
    {
     //   private readonly IIdentityManager _identityManager;

        #region Core

       // private readonly DataProviderServiceConfiguration _configurationProvider;
        private const string DefaultCountry = "default";

        //public CountryConfigurationProvider( IIdentityManager identityManager)
        //{
            
        //    if (identityManager == null)
        //        throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
        //           SubSystemError.GeneralRequiredParameterIsUndefined, "identityManager");
			
		  
        //    _identityManager = identityManager;
        //   // _configurationProvider =  ConfigurationProvider.ReadFromJson<DataProviderServiceConfiguration>(ConfigurationConstants.DataProviderServiceConfiguration);
        //}

        #endregion

        #region Public methods

        public static CountryConfiguration GetProviderConfigurationByCountry(string countryCode)
        {
			//var clientApplicationIdentity = _identityManager.GetClientApplicationIdentity();

			//if (clientApplicationIdentity == null)
			//    throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.SecurityClientApplicationNotAuthenticated, -1);

			var applicationId = 11;// clientApplicationIdentity.ApplicationId;

            var countryConfiguration = GetCountryConfiguration(countryCode, applicationId) ??
                                       GetCountryConfiguration(DefaultCountry, applicationId);

            if (countryConfiguration == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationContryConfigurationIsNotFound, applicationId, countryCode);

            return countryConfiguration;
        }

        /// <summary>
        /// Global application configuration provider instance
        /// </summary>
       // public IConfigurationProvider ConfigurationProvider { get; private set; }

        #endregion

        #region Private methods

        private static CountryConfiguration GetCountryConfiguration(string countryCode, int applicationId)
        {
			var config = Foundation.Providers.ConfigurationProvider.GetAppConfig<DataProviderServiceConfiguration>(ConfigurationConstants.DataProviderServiceConfiguration);

			return (from appConfig in config.ApplicationCountryConfigurations
                    where appConfig.ApplicationId == applicationId
                from country in appConfig.CountryConfigurations
                where country.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase)
                select country)
                .FirstOrDefault();
        }

        #endregion
    }
}
