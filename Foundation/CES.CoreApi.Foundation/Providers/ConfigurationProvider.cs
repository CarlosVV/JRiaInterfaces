using System;
using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Configuration;
using CES.CoreApi.Foundation.Contract.Constants;
using CES.CoreApi.Foundation.Contract.Interfaces;
using Newtonsoft.Json;
using ApplicationConfiguration = CES.CoreApi.Foundation.Contract.Models.ApplicationConfiguration;

namespace CES.CoreApi.Foundation.Providers
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly ICollection<ApplicationConfiguration> _configuration;

        public ConfigurationProvider(IApplicationRepository applicationRepository)
        {
            if (applicationRepository == null) throw new ArgumentNullException("applicationRepository");
            
            var applicationId = ConfigurationTools.ReadAppSettingsValue<int>(ServiceConfigurationItems.ApplicationId);
            _configuration = applicationRepository.GetApplicationConfiguration(applicationId).Result;
        }

		public static T GetAppConfig<T>(string configName)
		{
			//ConfigurationProvider.ReadFromJson<DataProviderServiceConfiguration>(ConfigurationConstants.DataProviderServiceConfiguration);
			IApplicationRepository r = new CES.CoreApi.Foundation.Repositories.ApplicationRepository();
			var applicationId = ConfigurationTools.ReadAppSettingsValue<int>(ServiceConfigurationItems.ApplicationId);
			var a  = r.GetApplicationConfiguration(applicationId).Result;

			foreach (var item in a)
			{
				if (string.IsNullOrEmpty(item.Name))
					continue;
				if(item.Name.Equals(configName))
				 return 	JsonConvert.DeserializeObject<T>(item.Value);
			}
			return default(T);
		}
        public T Read<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default(T);

            var item = _configuration.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return item == null ? default(T) : item.Value.ConvertValue<T>();
        }

        public T ReadFromJson<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return default(T);

            var item = Read<string>(name);

            return string.IsNullOrEmpty(item)
                ? default(T)
                : JsonConvert.DeserializeObject<T>(item);
        }
    }
}
