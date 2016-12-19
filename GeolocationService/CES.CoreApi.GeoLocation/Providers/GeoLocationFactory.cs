using CES.CoreApi.GeoLocation.ClientSettings;
using CES.CoreApi.GeoLocation.Configuration;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers.Bing;
using CES.CoreApi.GeoLocation.Providers.Google;
using CES.CoreApi.GeoLocation.Providers.MelissaData;
using CES.CoreApi.GeoLocation.Repositories;
using CES.CoreApi.GeoLocation.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Providers
{
	public class GeoLocationFactory
	{
		private const string DefaultCountry = "Default";
		public AddressValidationResponse Validation(Models.Requests.AddressRequest request)
		{

			IGeoLocationProvider provider = null;
			if (request.ProviderId == 1)
			{
				provider = new MelissaDataProvider();
			}
			else if (request.ProviderId == 2)
			{
				provider = new BingProvider();
			}
			else if (request.ProviderId == 3)
			{
				provider = new GoogleProvider();
			}
			else
			{
				provider = GetCountryConfiguration(request.Country,
				ClientHeader.GetApplicationId());

			}

			var result = provider.Validation(request);
			return result;
		}

		private static IGeoLocationProvider GetCountryConfiguration(string countryCode, int applicationId)
		{
			var cached = new ClientSettingRepositoryCached();
			var serviceData = cached.GetClientSetting(AppSettings.ApplicationId);
			if (serviceData == null)
				return new MelissaDataProvider();

			var clientSettings = JsonConvert.DeserializeObject<List<ClientAppSetting>>(serviceData);
			var useCountry = ClientHeader.IsCountryProviderSelection();
			ClientAppSetting clientSetting = null;

			if (!useCountry)
			{
				foreach (var item in clientSettings)
				{
					if (applicationId == item.ApplicationId)
					{
						clientSetting = item;
						break;
					}
						
				}
			}
			if (clientSetting == null)
			{
				if (!string.IsNullOrEmpty(countryCode))
				{
					if (countryCode.Length > 1)
						countryCode = DataSetting.GetCode(countryCode);

					foreach (var item in clientSettings)
					{
						if (countryCode == item.CountryCode)
						{
							clientSetting = item;
							break;
						}
					}
				}
			}
			if (clientSetting == null)
			{
				foreach (var item in clientSettings)
				{
					if (DefaultCountry.Equals(item.CountryCode, StringComparison.OrdinalIgnoreCase))
					{
						clientSetting = item;
						break;
					}
				}
			}

			if (clientSetting == null)
				return new MelissaDataProvider();
			foreach (var setting in clientSetting.DataProviders)
			{
				if (setting.DataProviderServiceType == Enumerations.DataProviderServiceType.Geocoding) {
					switch (setting.DataProviderType)
					{

						case Enumerations.DataProviderType.MelissaData:
							return new MelissaDataProvider();
						case Enumerations.DataProviderType.Bing:
							return new BingProvider();
						default:
							return new GoogleProvider();
					}

				}
			}
			if(clientSetting.DefaultGeoLocationProvider.Equals("Bing",StringComparison.OrdinalIgnoreCase))
			{
				return new BingProvider();
			}
			else if (clientSetting.DefaultGeoLocationProvider.Equals("MelissaData", StringComparison.OrdinalIgnoreCase))
			{
				return new MelissaDataProvider();
			}
			return new MelissaDataProvider();
		}
	}
}
