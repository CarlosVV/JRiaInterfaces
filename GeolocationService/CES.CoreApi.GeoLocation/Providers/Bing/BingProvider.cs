using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers.Shared;
using CES.CoreApi.GeoLocation.Repositories;
using CES.CoreApi.GeoLocation.Configuration;
using RestSharp.Extensions.MonoHttp;
using System.Globalization;
using CES.CoreApi.GeoLocation.Providers.Bing.JsonModels;
using Newtonsoft.Json;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Providers.Bing
{
	public class BingProvider : IGeoLocationProvider
	{

		private AddressRequestMode BuildUrl(AddressRequest address)
		{
			var requestBuilder = new AddressRequestMode { StateShort = address.AdministrativeArea };
			address.PostalCode = FuzzyMatch.ZipCodeValidation(address.PostalCode);
			string stateName = address.AdministrativeArea;

			ClientSettingRepository repo = new ClientSettingRepository();
			if (!string.IsNullOrEmpty(address.Country) && !string.IsNullOrEmpty(address.AdministrativeArea))
			{
				stateName = repo.GetStateName(1, address.AdministrativeArea, address.Country);
				if (!string.IsNullOrEmpty(stateName) && !requestBuilder.StateShort.Equals(stateName))
				{
					stateName = HttpUtility.UrlEncode(stateName);
				}
			}

			requestBuilder.RequestModifed = address;
			requestBuilder.RequestModifed.AdministrativeArea = stateName;
			var url = string.Format(CultureInfo.InvariantCulture,
			"{0}/Locations?CountryRegion={1}&adminDistrict={2}&locality={3}&postalCode={4}&addressLine={5}&o=json&include=ciso2&userIp=127.0.0.1&maxResults=1&key={6}",
			GeoLocationConfigurationSection.Instance.Bing.Url,
			HttpUtility.UrlEncode(address.Country),
			HttpUtility.UrlEncode(stateName),
			HttpUtility.UrlEncode(address.City),
			HttpUtility.UrlEncode(address.PostalCode),
			HttpUtility.UrlEncode(address.Address1),
			GeoLocationConfigurationSection.Instance.Bing.Key);

			requestBuilder.Url = url;
			return requestBuilder;
		}

		public AddressValidationResponse Validation(AddressRequest addressRequest)
		{
			IDataResponseProvider client = new DataResponseProvider();
			var requestMode = BuildUrl(addressRequest);
			var providerResponse = client.GetResponse(requestMode.Url);
			var result = JsonConvert.DeserializeObject<BingResult>(providerResponse.RawResponse);

			var response = new AddressValidationResponse
			{
				RowData = result,
				DataProviderName = "Bing",
				DataProvider = Enumerations.DataProviderType.Bing
			};

			if (result == null)
				return response;
		
			foreach (var item in result.resourceSets)
			{
				

				foreach (var bing in item.resources)
				{
					response.ResultCodes = $"confidence: {bing.confidence}, entityType: {bing.entityType}";
					response.ProviderMessage = $"{result.authenticationResultCode}, {result.statusDescription}";


						if (bing.point != null && bing.point.coordinates != null && bing.point.coordinates.Length >= 1)
						{
							response.Location = new LocationModel
							{
								Longitude = bing.point.coordinates[1],
								Latitude = bing.point.coordinates[0],
								LocationType = bing.point.type
							};
						}
						if (bing.address != null)
						{
							response.Address = new AddressModel
							{
								Address1 = bing.address.addressLine,
								AdministrativeArea = FuzzyMatch.RemoveLastDot(bing.address.adminDistrict),
								City = bing.address.locality,
								Country = bing.address.countryRegionIso2,
								PostalCode = bing.address.postalCode,
								FormattedAddress = bing.address.formattedAddress,
								CountryName = bing.address.countryRegion
							};

							response.AddressComponent = new Models.Responses.Validate.AddressComponent
							{
								StreetLongName = bing.address.addressLine,
								AdministrativeArea = bing.address.adminDistrict,
								Locality = bing.address.locality,
								LocalityLongName = bing.address.adminDistrict2,
								Country = bing.address.countryRegionIso2,
								CountryName = bing.address.countryRegion,
								PostalCode = bing.address.postalCode
							};



							/// Grading
							int distance = FuzzyMatch.GetGrade(response.Address.Address1, requestMode.RequestModifed.Address1);
							response.AddressMatch = distance;
							//
							if (!string.IsNullOrEmpty(response.Address.AdministrativeArea))
							{
								response.StateMatch = FuzzyMatch.GetGrade(response.Address.AdministrativeArea.Replace(".", ""), requestMode.StateShort);
							}

							if (response.StateMatch < 100)
							{
								distance = FuzzyMatch.GetGrade(response.Address.AdministrativeArea, requestMode.RequestModifed.AdministrativeArea);
								if (distance > response.StateMatch)
									response.StateMatch = distance;
							}

							//City
							response.CityMatch = FuzzyMatch.GetGrade(response.Address.City, addressRequest.City);
							//ctry
							response.CountryMatch = FuzzyMatch.GetGrade(response.Address.Country, addressRequest.Country);
							if (response.CountryMatch < 100)
							{
								distance = FuzzyMatch.GetGrade(response.Address.CountryName, addressRequest.Country);
								if (distance > response.CountryMatch)
									response.CountryMatch = distance;
							}
							//zip
							response.PostalCodeMatch = FuzzyMatch.GetGrade(response.Address.PostalCode, addressRequest.PostalCode);
						}

					break;
				}
			}

			return response;
		}
	}
}
