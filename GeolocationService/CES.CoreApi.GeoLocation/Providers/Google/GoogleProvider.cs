using CES.CoreApi.GeoLocation.Configuration;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers;
using CES.CoreApi.GeoLocation.Providers.Google.JsonModels;
using CES.CoreApi.GeoLocation.Providers.Shared;
using CES.CoreApi.GeoLocation.Repositories;
using CES.CoreApi.GeoLocation.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace CES.CoreApi.GeoLocation.Providers.Google
{
	public class GoogleProvider : IGeoLocationProvider
	{
		

	

		
		private AddressRequestMode BuildUrl(AddressRequest address)
		{
			var RequestMode = new AddressRequestMode();
			address.PostalCode =  FuzzyMatch.ZipCodeValidation(address.PostalCode);

	
			ClientSettingRepository repo = new ClientSettingRepository();
			RequestMode.StateShort = address.AdministrativeArea;
			var stateName =repo.GetStateName(1, address.AdministrativeArea, address.Country);

		

			var addressFormatted = $"{address.Address1.Replace(",", " ")}";
			if(!string.IsNullOrEmpty(address.PostalCode))
			{
				addressFormatted = $"{addressFormatted},{address.PostalCode}";
			}

			string state = string.Empty;
			if(!RequestMode.StateShort.Equals(stateName))
			{
				state = $"|administrative_area:{ HttpUtility.UrlEncode(stateName)}";
			}
			string client = string.Empty;
			if(!string.IsNullOrEmpty(AppSettings.GoogleClientId) && AppSettings.GoogleUseKeyForAddressValidate)
			{
				client = $"&client={AppSettings.GoogleClientId}";
			}
			var url = $"{AppSettings.GoogleMapGeocodeUrl}address={HttpUtility.UrlEncode(addressFormatted)}&components=country:{address.Country}|locality:{address.City}{state}{client}";

			RequestMode.RequestModifed = address;
			RequestMode.RequestModifed.AdministrativeArea = stateName;
			RequestMode.Url = url;
			if (!string.IsNullOrEmpty(client))
			{
				RequestMode.Url = SignUrl.Sign(url, AppSettings.GooglePrivateCryptoKey);
			}

			return RequestMode;
		}
		private bool HasAddressComponent(List<string> items, string key)
		{
			var result = from q in items where q.Equals(key, StringComparison.OrdinalIgnoreCase) select q;
			return result.Count() > 0;
		}

	

		public AddressValidationResponse Validation(AddressRequest addressRequest)
		{
			IDataResponseProvider client = new DataResponseProvider();
			var requestMode = BuildUrl(addressRequest);
			var providerResponse = client.GetResponse(requestMode.Url);	
			if(providerResponse != null&& providerResponse.RawResponse != null && providerResponse.RawResponse.ToLower().Contains("unable to authenticate the request."))
			{
				return new AddressValidationResponse { DataProviderName = "Google",
					DataProvider = Enumerations.DataProviderType.Google,
					ResultCodes= providerResponse.RawResponse,
					ProviderMessage = providerResponse.RawResponse };
			}
			var result = JsonConvert.DeserializeObject<GeocodeResults>(providerResponse.RawResponse);
			var response = new AddressValidationResponse();
			var others = new List<SeeAlso>();

			var addressComponent = null as Models.Responses.Validate.AddressComponent;	
			var location = null as LocationModel;
			var resultCode = null as string;

			int weight = 0;
			foreach (var item in result.results)
			{
				weight = 0;
				location = new LocationModel();

				resultCode = string.Join(",", item.types);
				if (item.geometry != null)
				{
					if (item.geometry.location != null)
					{
						location.Latitude = item.geometry.location.lat;
						location.Longitude = item.geometry.location.lng;
					}
					location.LocationType = item.geometry.location_type;

					resultCode = $"{resultCode},{item.geometry.location_type}";
				}
				addressComponent = new Models.Responses.Validate.AddressComponent();
				addressComponent.FormattedAddress = FuzzyMatch.RemoveDiacritics(item.formatted_address);
			
				foreach (var address in item.address_components)
				{
					if (HasAddressComponent(address.types, GoogleConstants.StreetNumber))
					{
						addressComponent.StreetNumber = address.short_name;
					
					}
					if (HasAddressComponent(address.types, GoogleConstants.Street))
					{
						addressComponent.Street = address.short_name;
						addressComponent.StreetLongName = address.long_name;
						
						addressComponent.AddressDistance = GetGrade($"{addressComponent.StreetNumber} {addressComponent.Street}", addressRequest.Address1);
						if (addressComponent.AddressDistance < 100)
						{
							weight = GetGrade($"{addressComponent.Street} {addressComponent.StreetNumber}", addressRequest.Address1);
							if (weight > addressComponent.AddressDistance)
								addressComponent.AddressDistance = weight;
						}
						if (addressComponent.AddressDistance < 100)
						{
							weight = GetGrade($"{addressComponent.StreetNumber} {addressComponent.StreetLongName}", addressRequest.Address1);
							if (weight > addressComponent.AddressDistance)
								addressComponent.AddressDistance = weight;
						}

						if (addressComponent.AddressDistance < 100)
						{
							weight = GetGrade($"{addressComponent.StreetLongName} {addressComponent.StreetNumber} ", addressRequest.Address1);
							if (weight > addressComponent.AddressDistance)
								addressComponent.AddressDistance = weight;
						}



					}
					if (HasAddressComponent(address.types, GoogleConstants.City))
					{
						addressComponent.Locality = FuzzyMatch.RemoveDiacritics(address.short_name);
						addressComponent.LocalityLongName = address.long_name;

						addressComponent.CityDistance = GetGrade(addressComponent.Locality, addressRequest.City);
						if (addressComponent.CityDistance < 100)
						{
							weight = GetGrade(addressComponent.LocalityLongName, addressRequest.City);
							if (weight > addressComponent.CityDistance)
								addressComponent.CityDistance = weight;
						}

					}
					if (HasAddressComponent(address.types, GoogleConstants.AdministrativeArea))
					{
						addressComponent.AdministrativeArea = address.short_name;
						addressComponent.AdministrativeAreaLongName = address.long_name;
						if (address.short_name.EndsWith("."))
						{
						
							addressComponent.StateDistance = GetGrade(address.short_name.Replace(".", ""), requestMode.StateShort);
						}

						if (addressComponent.StateDistance < 100)
						{

							weight = GetGrade(addressComponent.AdministrativeArea, requestMode.RequestModifed.AdministrativeArea);
							if (weight > addressComponent.StateDistance)
								addressComponent.StateDistance = weight;
						}
						if (addressComponent.StateDistance < 100)
						{
							weight = GetGrade(addressComponent.AdministrativeAreaLongName, requestMode.RequestModifed.AdministrativeArea);
							if (weight > addressComponent.StateDistance)
								addressComponent.StateDistance = weight;
						}
					}

					if (HasAddressComponent(address.types, GoogleConstants.Country))
					{
						addressComponent.Country = address.short_name;
						addressComponent.CountryName = address.long_name;
					}

					if (HasAddressComponent(address.types, GoogleConstants.PostalCode))
					{
						addressComponent.PostalCode = address.short_name;
						addressComponent.PostalCodeLongName = address.long_name;
						addressComponent.PostalCodeDistance = GetGrade(address.short_name, requestMode.RequestModifed.PostalCode);

						if (addressComponent.PostalCodeDistance < 100)
						{
							weight = GetGrade(address.long_name, requestMode.RequestModifed.PostalCode);
							if (weight > addressComponent.PostalCodeDistance)
								addressComponent.PostalCodeDistance = weight;
						}
					}
				
				}
				var other = new SeeAlso { Location = location, AddressComponents = addressComponent, Types = string.Join(",", item.types), ResultCodes = resultCode, Weight = weight };
			
				others.Add(other);
			}

			var pick = FuzzyMatch.CorePick(others, addressRequest);
			if (pick != null)
			{
				response.AddressComponent = pick.MainPick.AddressComponents;
				response.Location = pick.MainPick.Location;
				response.SeeAlso = pick.Alternates;
				//response.Distance = pick.MainPick.Weight;
				response.ResultCodes = pick.MainPick.ResultCodes;

				response.Address = new AddressModel
				{
					Address1 = GetGradeAddress(response.AddressComponent,""),
					AdministrativeArea = FuzzyMatch.RemoveLastDot(response.AddressComponent.AdministrativeArea),
					City = response.AddressComponent.Locality,
					Country = response.AddressComponent.Country,
					PostalCode = response.AddressComponent.PostalCode,
					FormattedAddress = response.AddressComponent.FormattedAddress,
					CountryName = response.AddressComponent.CountryName

				};

				if(response.AddressComponent != null)
				{
					response.CountryMatch = GetGrade(response.AddressComponent.CountryName, requestMode.RequestModifed.Country);
					if(response.CountryMatch <100)
						response.CountryMatch = GetGrade(response.AddressComponent.Country, requestMode.RequestModifed.Country);

					response.StateMatch = response.AddressComponent.StateDistance;
					response.CityMatch = response.AddressComponent.CityDistance;
					response.PostalCodeMatch = response.AddressComponent.PostalCodeDistance ==0? GetGrade(response.AddressComponent.PostalCode, requestMode.RequestModifed.PostalCode)
							: response.AddressComponent.PostalCodeDistance;
					response.AddressMatch = response.AddressComponent.AddressDistance;

				
				}
				if (response.AddressMatch < 100)
				{
					int dis = GetGrade(response.Address.Address1, requestMode.RequestModifed.Address1);
					if (dis > response.AddressMatch)
						response.AddressMatch = dis;
				}


			}
			response.RowData = result;
			response.ProviderMessage = result.status;			
			response.DataProvider = Enumerations.DataProviderType.Google;
			response.DataProviderName = "Google";
			return response;
		}

		

		

		private string GetGradeAddress(Models.Responses.Validate.AddressComponent comp, string requestMode)
		{
			if (comp == null)
				return string.Empty;
			
			string value = comp.FormattedAddress;
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			if(value.EndsWith("USA",StringComparison.OrdinalIgnoreCase))
				value = value.Replace("USA", "");

			if (!string.IsNullOrEmpty(comp.AdministrativeArea))
				value = value.Replace(comp.AdministrativeArea, "");
			if (!string.IsNullOrEmpty(comp.AdministrativeAreaLongName))
				value = value.Replace(comp.AdministrativeAreaLongName, "");
			if (!string.IsNullOrEmpty(comp.Locality))
				value = value.Replace(comp.Locality, "");
			if (!string.IsNullOrEmpty(comp.LocalityLongName))
				value = value.Replace(comp.LocalityLongName, "");
			if (!string.IsNullOrEmpty(comp.Country))
				value = value.Replace(comp.Country, "");
			if (!string.IsNullOrEmpty(comp.CountryName))
				value = value.Replace(comp.CountryName, "");
			if (!string.IsNullOrEmpty(comp.PostalCode))
				value = value.Replace(comp.PostalCode, "");

			if (string.IsNullOrEmpty(value))
				return string.Empty;




			char[] ch = { ',' };
			string[] rs = value.Trim().Split(ch, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder sb = new StringBuilder();
			int count = 0;
			foreach (var item in rs)
			{
				if (string.IsNullOrEmpty(item.Trim()))
					continue;
				count++;
				if (count > 1)
				{
					sb.Append($", {item}");
				}
				else {
				sb.Append(item);
				}
			}

			return sb.ToString();

			


		}

		private int GetGrade(string google, string requestMode)
		{

			if (string.IsNullOrEmpty(google) && string.IsNullOrEmpty(requestMode))
				return 100;

			if (!string.IsNullOrEmpty(google) && string.IsNullOrEmpty(requestMode))
				return 110;



			if (string.IsNullOrEmpty(google))
				return 0;

			if (string.IsNullOrEmpty(requestMode))
				return 0;

			if (google.Trim().Equals(requestMode.Trim(), StringComparison.OrdinalIgnoreCase))
				return 100;

			JaroWrinklerDistance distance = new JaroWrinklerDistance();
			var f = distance.Apply(FuzzyMatch.RemoveDiacritics(google).ToLower().Trim(), FuzzyMatch.RemoveDiacritics(requestMode).ToLower().Trim());
		
			return Convert.ToInt16(f *100);


		}
	
		
	}
	
}
