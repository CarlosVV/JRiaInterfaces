using System;
using CES.CoreApi.GeoLocation.Configuration;
using CES.CoreApi.Security.Providers;
using CES.CoreApi.GeoLocation.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using CES.CoreApi.GeoLocation.ClientSettings;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class CountryConfigurationProvider 
    {

		private readonly ClientSettingRepository repository;
		private static readonly Lazy<CountryConfigurationProvider> instance = new Lazy<CountryConfigurationProvider>(
			() => new CountryConfigurationProvider(new ClientSettingRepository()));


		private CountryConfigurationProvider(ClientSettingRepository repository)
		{
			this.repository = repository;
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



        #endregion

        #region Private methods

        private  static ClientAppSetting GetCountryConfiguration(string countryCode, int applicationId)
        {
			var serviceData = instance.Value.repository.GetClientSetting(AppSettings.ApplicationId);
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
