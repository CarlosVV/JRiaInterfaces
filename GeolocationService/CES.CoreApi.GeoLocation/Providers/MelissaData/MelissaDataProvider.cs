using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers.MelissaData.JsonModels;
using CES.CoreApi.GeoLocation.Providers.Shared;
using CES.CoreApi.GeoLocation.Repositories;
using Newtonsoft.Json;
using RestSharp.Extensions.MonoHttp;
using System.Configuration;

namespace CES.CoreApi.GeoLocation.Providers.MelissaData
{
	public class MelissaDataProvider: IGeoLocationProvider
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
			var url = string.Format(
			   "{0}?format=json&id={1}&a1={2}&loc={3}&admarea={4}&postal={5}&ctry={6}&opt=DeliveryLines:ON",
			   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressUrl"],
			   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressKey"],
			   HttpUtility.UrlEncode(address.Address1),
			   HttpUtility.UrlEncode(address.City),
			   HttpUtility.UrlEncode(stateName),
			   HttpUtility.UrlEncode(address.PostalCode),
			   HttpUtility.UrlEncode(address.Country));


			requestBuilder.Url = url;
			return requestBuilder;
		}

		private double GetSafeDouble(string value)
		{
			double id;
			double.TryParse(value, out id);
			return id;
		}
		public AddressValidationResponse Validation(AddressRequest addressRequest)
		{
			IDataResponseProvider client = new DataResponseProvider();
			var requestMode = BuildUrl(addressRequest);
			var providerResponse = client.GetResponse(requestMode.Url);
			var result = JsonConvert.DeserializeObject<GlobalAddressModel>(providerResponse.RawResponse);
			var response = new AddressValidationResponse
			{
				RowData = result,
				DataProvider = Enumerations.DataProviderType.MelissaData,
				DataProviderName = "MelissaData",
				ProviderMessage = result.TransmissionResults
			};
		


			if (result.Records != null)
			{
				foreach (var item in result.Records)
				{
					response.ResultCodes = item.Results;
					response.Location = new LocationModel
					{
						Latitude = GetSafeDouble(item.Latitude),
						Longitude = GetSafeDouble(item.Longitude)
					};
					response.AddressComponent = new Models.Responses.Validate.AddressComponent
					{
						StreetNumber = item.PremisesNumber,
						Street = item.Thoroughfare,
						StreetLongName = item.AddressLine1,
						Locality = item.Locality,
						AdministrativeArea = item.AdministrativeArea,
						Country = item.CountryISO3166_1_Alpha2,
						CountryName = item.CountryName,
						FormattedAddress = item.FormattedAddress,
						PostalCode = item.PostalCode

					};
					response.Address = new AddressModel
					{
						Address1 = item.AddressLine1,
						AdministrativeArea = item.AdministrativeArea,
						City = item.Locality,
						Country = item.CountryISO3166_1_Alpha2,
						CountryName = item.CountryName,
						FormattedAddress = item.FormattedAddress,
						PostalCode = item.PostalCode,
						UnitOrApartment = item.PremisesNumber

					};

					//Grading 
					//Address Line
					int distance = FuzzyMatch.GetGrade(response.Address.Address1, addressRequest.Address1);
					response.AddressMatch = distance;
					
					//State 
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
					
					//Country 
					response.CountryMatch = FuzzyMatch.GetGrade(response.Address.Country, addressRequest.Country);
					if (response.CountryMatch < 100)
					{
						distance = FuzzyMatch.GetGrade(response.Address.CountryName, addressRequest.Country);
						if (distance > response.CountryMatch)
							response.CountryMatch = distance;
					}
					//Postal Code
					response.PostalCodeMatch = FuzzyMatch.GetGrade(response.Address.PostalCode, addressRequest.PostalCode);

					break;
				}
			}


			return response;

		}
		
	
	}
}

