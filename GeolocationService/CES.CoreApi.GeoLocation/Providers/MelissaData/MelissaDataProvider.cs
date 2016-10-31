using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers.MelissaData.JsonModels;
using CES.CoreApi.GeoLocation.Providers.Shared;
using Newtonsoft.Json;
using RestSharp.Extensions.MonoHttp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers.MelissaData
{
	public class MelissaDataProvider
	{
		private string BuildUrl(AddressRequest address)
		{
			var url = string.Format(
			   "{0}?format=json&id={1}&a1={2}&loc={3}&admarea={4}&postal={5}&ctry={6}&opt=DeliveryLines:ON",
			   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressUrl"],
			   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressKey"],
			   HttpUtility.UrlEncode(address.Address1),		
			   HttpUtility.UrlEncode(address.City),
			   HttpUtility.UrlEncode(address.AdministrativeArea),
			   HttpUtility.UrlEncode(address.PostalCode),
			   HttpUtility.UrlEncode(address.Country));

			return url;
		}
		private double GetSafeDouble(string value)
		{
			double id;
			double.TryParse(value, out id);
			return id;
		}
		public AddressValidationResponse DoValidation(AddressRequest addressRequest)
		{
			IDataResponseProvider client = new DataResponseProvider();
			var providerResponse = client.GetResponse(BuildUrl(addressRequest));
			var result = JsonConvert.DeserializeObject<GlobalAddressModel>(providerResponse.RawResponse);
			var response = new AddressValidationResponse();
			var others = new List<SeeAlso>();
			var addressComponent = null as Models.Responses.Validate.AddressComponent;
			var location = null as LocationModel;
			var resultCode = null as string;

			if (result.Records != null)
			{
				foreach (var item in result.Records)
				{
					location = new LocationModel();
					location.Latitude = GetSafeDouble(item.Longitude);
					location.Longitude = GetSafeDouble(item.Latitude);
					addressComponent = new Models.Responses.Validate.AddressComponent
					{
						StreetNumber = item.PremisesNumber,
						Street = item.Thoroughfare,
						StreetLongName = item.AddressLine1,
						Locality = item.Locality,
						AdministrativeArea = item.AdministrativeArea,
						Country = item.CountryISO3166_1_Alpha2,
						CountryName = item.CountryName,
						FormattedAddress = item.FormattedAddress,
						PostalCode = item.PostalCode,

					};
					addressComponent.FormattedAddress = item.FormattedAddress;

					var other = new SeeAlso
					{
						Location = location,
						AddressComponents = addressComponent,
						Types = "",
						ResultCodes = item.Results,
						Weight = 0
					};

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
				}
			}

			response.RowData = result;		
			response.ProviderMessage = result.TransmissionResults;			
			response.DataProvider = Enumerations.DataProviderType.MelissaData;
			response.DataProviderName = "MelissaData";
			return response;
			
		}
		
	}
}

