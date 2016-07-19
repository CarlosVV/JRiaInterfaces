using System;
using CES.CoreApi.GeoLocation.Configuration;
using CES.CoreApi.GeoLocation.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using CES.CoreApi.GeoLocation.ClientSettings;
using System.Security.Principal;
using System.Threading;
using System.Reflection;

namespace CES.CoreApi.GeoLocation.Logic.Providers
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
		
			IIdentity identity = Thread.CurrentPrincipal.Identity;
			int appId = 0;

			Type type = identity.GetType();
			var prop = type.GetProperty("ApplicationId",
										 BindingFlags.Public
										 | BindingFlags.Instance
										 | BindingFlags.IgnoreCase);

			if (prop != null)
				appId = (int)prop.GetValue(identity, null);
			
			var setting = GetCountryConfiguration(countryCode, appId);
			if (setting != null)
				return setting;

			return GetCountryConfiguration(DefaultCountry, appId);
		
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
