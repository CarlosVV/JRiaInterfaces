using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers.Google.JsonModels;
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
				address.AdministrativeArea,
				address.PostalCode
				);

			char[] ch = { ',' };
			string[] addresses = addressFormatted.Split(ch, System.StringSplitOptions.RemoveEmptyEntries);

			addressFormatted = string.Join(",", addressFormatted);
			var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={System.Web.HttpUtility.UrlEncode(addressFormatted)}&components=country:{address.Country}";
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
			var response = client.GetResponse(BuildUrl(addressRequest));
			AddressValidationResponse clientRepo = new AddressValidationResponse();
			var result = JsonConvert.DeserializeObject<GeocodeResults>(response.RawResponse);
			dynamic expando = new ExpandoObject();
			clientRepo.RowData = result;

		
			var location = new LocationModel();
			clientRepo.ProviderMessage = result.status;
			foreach (var item in result.results)
			{
				if (item.geometry != null && item.geometry.location != null)
				{
					location.Latitude = item.geometry.location.lat;
					location.Longitude = item.geometry.location.lng;
				}

				expando.FormattedAddress = item.formatted_address;
				clientRepo.ResultCodes = string.Join(",", item.types);
				foreach (var address in item.address_components)
				{
					if (HasAddressComponent(address.types, GoogleConstants.StreetNumber))
					{
						expando.StreetNumber = address.short_name;
					}
					if (HasAddressComponent(address.types, GoogleConstants.Street))
					{
						expando.Street = address.short_name;
						expando.StreetLongName = address.long_name;
					}
					if (HasAddressComponent(address.types, GoogleConstants.City))
					{
						expando.Locality = address.short_name;
						expando.LocalityLongName = address.long_name;
					}
					if (HasAddressComponent(address.types, GoogleConstants.AdministrativeArea))
					{
						expando.AdministrativeArea = address.short_name;
						expando.AdministrativeAreaLongName = address.long_name;
					}
					if (HasAddressComponent(address.types, GoogleConstants.Country))
					{
						expando.Country = address.short_name;
						expando.CountryName = address.long_name;
					}
					if (HasAddressComponent(address.types, GoogleConstants.PostalCode))
					{
						expando.PostalCode = address.short_name;
					}
				}
			}
			clientRepo.AddressComponent = expando;
			clientRepo.Location = location;
			return clientRepo;
		}
		
	}
	
}
