using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Models.Responses.Validate;
using CES.CoreApi.GeoLocation.Providers.Google.JsonModels;
using CES.CoreApi.GeoLocation.Providers.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace CES.CoreApi.GeoLocation.Providers
{
	public class GoogleProvider
	{

		private string BuildUrl(AddressRequest address)
		{
			var addressFormatted = string.Join(",",
				address.Address1,
				address.Address2,
				address.City,			
				address.PostalCode
				);

			char[] ch = { ',' };
			string[] addresses = addressFormatted.Split(ch, System.StringSplitOptions.RemoveEmptyEntries);

			addressFormatted = string.Join(",", addresses);
			var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={addressFormatted}&components=country:{address.Country}";
			return url;
		}
		private bool HasAddressComponent(List<string> items, string key)
		{
			var result = from q in items where q.Equals(key, StringComparison.OrdinalIgnoreCase) select q;
			return result.Count() > 0;
		}

	

		public AddressValidationResponse DoValidation(AddressRequest addressRequest)
		{
			IDataResponseProvider client = new DataResponseProvider();
			var providerResponse = client.GetResponse(BuildUrl(addressRequest));	
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

					resultCode = $"{response.ResultCodes},{item.geometry.location_type}";
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
				response.Weight = pick.MainPick.Weight;
			}
		
			response.RowData = result;
			response.ResultCodes = resultCode;
			response.ProviderMessage = result.status;			
			response.DataProvider = Enumerations.DataProviderType.Google;
			response.DataProviderName = "Google";
			return response;
		}
		
	}
	
}
