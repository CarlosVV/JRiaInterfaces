using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers.Google.JsonModels;
using CES.CoreApi.GeoLocation.Providers.Shared;
using CES.CoreApi.GeoLocation.Repositories;
using CES.CoreApi.GeoLocation.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CES.CoreApi.GeoLocation.Providers
{
	public class GoogleProvider
	{
		class AddressRequestMode
		{
			public AddressRequest RequestModifed { get; set; }
			public string StateShort { get; set; }
			public string Url { get; set; }
		}

		private string ZipCodeValidation(string zip)
		{
			if (string.IsNullOrEmpty(zip))
				return string.Empty;
			string result = "";
			int count = 0;
			foreach (var item in zip)
			{
				if (item != '0')
				{
					result = zip.Substring(count);
					return result;
				}
				count++;
			}


			return string.Empty;
		}

		
		private AddressRequestMode BuildUrl(AddressRequest address)
		{
			var RequestMode = new AddressRequestMode();
			address.PostalCode = ZipCodeValidation(address.PostalCode);

			//var addressFormatted = string.Join(",",
			//	address.Address1,								
			//	address.PostalCode
			//	);
			ClientSettingRepository repo = new ClientSettingRepository();
			RequestMode.StateShort = address.AdministrativeArea;
			var stateName =repo.GetStateName(1, address.AdministrativeArea, address.Country);

			//char[] ch = { ',' };
			//string[] addresses = addressFormatted.Split(ch, System.StringSplitOptions.RemoveEmptyEntries);

			var addressFormatted = $"{address.Address1.Replace(",", "")}";
			if(!string.IsNullOrEmpty(address.PostalCode))
			{
				addressFormatted = $"{addressFormatted},{address.PostalCode}";
			}

			var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={HttpUtility.UrlEncode(addressFormatted)}&components=country:{address.Country}|locality:{address.City}|administrative_area:{HttpUtility.UrlEncode(stateName)}";

			RequestMode.RequestModifed = address;
			RequestMode.RequestModifed.AdministrativeArea = stateName;
	
			RequestMode.Url = url;

			return RequestMode;
		}
		private bool HasAddressComponent(List<string> items, string key)
		{
			var result = from q in items where q.Equals(key, StringComparison.OrdinalIgnoreCase) select q;
			return result.Count() > 0;
		}

	

		public AddressValidationResponse DoValidation(AddressRequest addressRequest)
		{
			IDataResponseProvider client = new DataResponseProvider();
			var requestMode = BuildUrl(addressRequest);
			var providerResponse = client.GetResponse(requestMode.Url);	
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
				addressComponent.FormattedAddress = item.formatted_address;
			
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
						weight += FuzzyMatch.Compute(addressComponent.Street, addressRequest.Address1);
						weight += FuzzyMatch.Compute(addressComponent.StreetLongName, addressRequest.Address1);
					}
					if (HasAddressComponent(address.types, GoogleConstants.City))
					{
						addressComponent.Locality = address.short_name;
						addressComponent.LocalityLongName = address.long_name;

					}
					if (HasAddressComponent(address.types, GoogleConstants.AdministrativeArea))
					{
						addressComponent.AdministrativeArea = address.short_name;
						addressComponent.AdministrativeAreaLongName = address.long_name;
						weight += FuzzyMatch.Compute(addressComponent.AdministrativeArea, addressRequest.AdministrativeArea);
						weight += FuzzyMatch.Compute(addressComponent.AdministrativeAreaLongName, addressRequest.AdministrativeArea);
					}
					if (HasAddressComponent(address.types, GoogleConstants.Country))
					{
						addressComponent.Country = address.short_name;
						addressComponent.CountryName = address.long_name;
					}
					if (HasAddressComponent(address.types, GoogleConstants.PostalCode))
					{
						addressComponent.PostalCode = address.short_name;
						weight += FuzzyMatch.Compute(address.short_name, addressRequest.PostalCode);

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
				response.Distance = pick.MainPick.Weight;

			
				response.Address = new AddressModel
				{
					Address1 = GetGradeAddress(response.AddressComponent,""),
					AdministrativeArea = response.AddressComponent.AdministrativeArea,
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

					response.StateMatch = GetGrade(response.AddressComponent.AdministrativeAreaLongName, requestMode.RequestModifed.AdministrativeArea);
					
					if (response.StateMatch < 100)
					{
						double stateMatch2 = GetGrade(response.AddressComponent.AdministrativeArea, requestMode.StateShort);
						if (stateMatch2 > response.StateMatch)
							response.StateMatch = stateMatch2;
						
					}


					response.CityMatch = GetGrade(response.AddressComponent.Locality, requestMode.RequestModifed.City);

					if (response.CityMatch < 100)
					{
						double stateMatch2 = GetGrade(response.AddressComponent.LocalityLongName, requestMode.RequestModifed.City);
						if (stateMatch2 > response.CityMatch)
							response.CityMatch = stateMatch2;

					}


					response.PostalCodeMatch = GetGrade(response.AddressComponent.PostalCode, requestMode.RequestModifed.PostalCode);
				}

				response.AddressMatch = GetGrade(response.Address.Address1, requestMode.RequestModifed.Address1);


			}
			response.RowData = result;
			response.ResultCodes = resultCode;
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

			//return string.Join(",", rs).Trim();


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
			var f = distance.Apply(google.ToLower().Trim(), requestMode.ToLower().Trim());
			//var f = FuzzyMatch.Compute(google.ToLower().Trim(), requestMode.ToLower().Trim());
			if (f <= 0)
				return 100;

		//	double   g =  100- ((double)f/ (double)google.Length *100);
			return Convert.ToInt16(f *100);


		}
		private string GetAddress1(Models.Responses.Validate.AddressComponent comp)
		{
			if (comp == null)
				return string.Empty;

			if (string.IsNullOrEmpty(comp.FormattedAddress) || string.IsNullOrEmpty(comp.Street))
				return null;


		
			char[] ch = { ',' };

			var  addresses = comp.FormattedAddress.Split(ch, StringSplitOptions.RemoveEmptyEntries);
			if (addresses.Length == 1)
				return string.Empty;

			

				var items = new List<string>();
			string value;
		
			foreach (var item in addresses)
			{
				if (item.Contains(comp.Street) || item.Contains(comp.StreetLongName))
					return item;

			
				value = item.Trim(); 

				if (!string.IsNullOrEmpty(comp.AdministrativeArea) && value.Equals(comp.AdministrativeArea, StringComparison.OrdinalIgnoreCase))
					continue;
				if (!string.IsNullOrEmpty(comp.AdministrativeAreaLongName) && value.Equals(comp.AdministrativeAreaLongName, StringComparison.OrdinalIgnoreCase))
					continue;
				if (!string.IsNullOrEmpty(comp.Locality) && value.Equals(comp.Locality, StringComparison.OrdinalIgnoreCase))
					continue;
				if (!string.IsNullOrEmpty(comp.LocalityLongName) && value.Equals(comp.LocalityLongName, StringComparison.OrdinalIgnoreCase))
					continue;
				if (!string.IsNullOrEmpty(comp.Country) && value.Equals(comp.Country, StringComparison.OrdinalIgnoreCase))
					continue;
				if (!string.IsNullOrEmpty(comp.CountryName) && value.Equals(comp.CountryName, StringComparison.OrdinalIgnoreCase))
					continue;
				if (!string.IsNullOrEmpty(comp.PostalCode) && value.Equals(comp.PostalCode, StringComparison.OrdinalIgnoreCase))
					continue;
				if (!string.IsNullOrEmpty(comp.PostalCode) && value.EndsWith(comp.PostalCode))
					continue;


				items.Add(item);

			}
			if(items.Count ==1)
				return items[0];

			List<int> rank = new List<int>();
			int min = 0;
			for (int  i=0; i < items.Count; i++)
			{
				int m = FuzzyMatch.Compute(items[i].ToLower().Trim(), comp.Street.ToLower().Trim());
				if (min < m)
				{
					min = i;
				}
					
				rank.Add(m);
				
			}

			return items[min];

			
			
		}
		
	}
	
}
