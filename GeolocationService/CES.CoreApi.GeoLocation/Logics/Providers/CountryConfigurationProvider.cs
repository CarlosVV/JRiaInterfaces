using System;
using System.Linq;
//using CES.CoreApi.Common.Enumerations;
//using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Configuration;
using CES.CoreApi.Security.Providers;
using CES.CoreApi.GeoLocation.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class CountryConfigurationProvider 
    {

		private readonly ServiceProviderRepository repo;
		private static readonly Lazy<CountryConfigurationProvider> instance = new Lazy<CountryConfigurationProvider>(
			() => new CountryConfigurationProvider(new ServiceProviderRepository()));


		private CountryConfigurationProvider(ServiceProviderRepository repo)
		{
			this.repo = repo;
		}
		#region Core
		// private readonly DataProviderServiceConfiguration _configurationProvider;
		private const string DefaultCountry = "default";
		#endregion

		#region Public methods

		public static ClientAppSetting GetProviderConfigurationByCountry(string countryCode)
        {
			var id = new IdentityProvider();
			var clientApplicationIdentity = id.GetClientApplicationIdentity();

		
			var applicationId = clientApplicationIdentity.ApplicationId;

            var countryConfiguration = GetCountryConfiguration(countryCode, applicationId) ??
                                       GetCountryConfiguration(DefaultCountry, applicationId);

            //if (countryConfiguration == null)
            //    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
            //        SubSystemError.GeolocationContryConfigurationIsNotFound, applicationId, countryCode);

            return countryConfiguration;
        }

        /// <summary>
        /// Global application configuration provider instance
        /// </summary>
       // public IConfigurationProvider ConfigurationProvider { get; private set; }

        #endregion

        #region Private methods

        private  static ClientAppSetting GetCountryConfiguration(string countryCode, int applicationId)
        {

			var serviceData = instance.Value.repo.GetServiceProvider(Settings.ApplicationId);
			var clientSettings = JsonConvert.DeserializeObject<List<ClientAppSetting>>(serviceData);

			foreach (var item in clientSettings)
			{
				if (applicationId == item.ApplicationId)
					return item;
			}
			
			return null;
        }

	

		#endregion
	}
}
