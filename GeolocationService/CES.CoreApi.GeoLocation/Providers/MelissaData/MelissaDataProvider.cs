using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Providers;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Models.Requests;
using CES.CoreApi.GeoLocation.Models.Responses;
using CES.CoreApi.GeoLocation.Providers.MelissaData.JsonModels;
using CES.CoreApi.GeoLocation.Providers.Shared;
using CES.CoreApi.GeoLocation.Repositories;
using CES.CoreApi.GeoLocation.Tools;
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
	public class MelissaDataProvider: IGeoLocationProvider
	{
		//private string BuildUrl(AddressRequest address)
		//{
		//	var url = string.Format(
		//	   "{0}?format=json&id={1}&a1={2}&loc={3}&admarea={4}&postal={5}&ctry={6}&opt=DeliveryLines:ON",
		//	   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressUrl"],
		//	   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressKey"],
		//	   HttpUtility.UrlEncode(address.Address1),		
		//	   HttpUtility.UrlEncode(address.City),
		//	   HttpUtility.UrlEncode(address.AdministrativeArea),
		//	   HttpUtility.UrlEncode(address.PostalCode),
		//	   HttpUtility.UrlEncode(address.Country));

		//	return url;
		//}


	private AddressRequestMode BuildUrl(AddressRequest address)
		{
			var RequestMode = new AddressRequestMode();
			address.PostalCode =  FuzzyMatch.ZipCodeValidation(address.PostalCode);

			string state = address.AdministrativeArea;
			string stateName = address.AdministrativeArea;

			ClientSettingRepository repo = new ClientSettingRepository();
			if (!string.IsNullOrEmpty(address.Country) && !string.IsNullOrEmpty(address.AdministrativeArea))
			{
				RequestMode.StateShort = address.AdministrativeArea;
				 stateName = repo.GetStateName(1, address.AdministrativeArea, address.Country);

				if (!string.IsNullOrEmpty(stateName) && !RequestMode.StateShort.Equals(stateName))
				{
					state = HttpUtility.UrlEncode(stateName);
				}
				
			}

			RequestMode.RequestModifed = address;
			RequestMode.RequestModifed.AdministrativeArea = stateName;
			var url = string.Format(
			   "{0}?format=json&id={1}&a1={2}&loc={3}&admarea={4}&postal={5}&ctry={6}&opt=DeliveryLines:ON",
			   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressUrl"],
			   ConfigurationManager.AppSettings["MelissaDataDoGlobalAddressKey"],
			   HttpUtility.UrlEncode(address.Address1),
			   HttpUtility.UrlEncode(address.City),
			   HttpUtility.UrlEncode(state),
			   HttpUtility.UrlEncode(address.PostalCode),
			   HttpUtility.UrlEncode(address.Country));


	
		
	
			RequestMode.Url = url;

			return RequestMode;
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

					response.Address = new AddressModel
					{
						Address1 = response.AddressComponent.StreetLongName,
						AdministrativeArea = FuzzyMatch.RemoveLastDot(response.AddressComponent.AdministrativeArea),
						City = response.AddressComponent.Locality,
						Country = response.AddressComponent.Country,
						PostalCode = response.AddressComponent.PostalCode,
						FormattedAddress = response.AddressComponent.FormattedAddress,
						CountryName = response.AddressComponent.CountryName

					};

					//Grading 
					int dis = GetGrade(response.Address.Address1, requestMode.RequestModifed.Address1);					
					response.AddressMatch = dis;
					//
					if (!string.IsNullOrEmpty(response.Address.AdministrativeArea))
					{

						response.StateMatch = GetGrade(response.Address.AdministrativeArea.Replace(".", ""), requestMode.StateShort);
					}

					if (response.StateMatch < 100)
					{
						dis = GetGrade(response.Address.AdministrativeArea, requestMode.RequestModifed.AdministrativeArea);
						if (dis > response.StateMatch)
							response.StateMatch = dis;
					}
					
					//City
					response.CityMatch = GetGrade(response.Address.City, addressRequest.City);
					//ctry
					response.CountryMatch = GetGrade(response.Address.Country, addressRequest.Country);
					if (response.CountryMatch < 100)
					{
						dis = GetGrade(response.Address.CountryName, addressRequest.Country);
						if (dis > response.CountryMatch)
							response.CountryMatch = dis;
					}
					//zip
					response.PostalCodeMatch = GetGrade(response.Address.PostalCode, addressRequest.PostalCode);
				}
			}

			response.RowData = result;		
			response.ProviderMessage = result.TransmissionResults;			
			response.DataProvider = Enumerations.DataProviderType.MelissaData;
			response.DataProviderName = "MelissaData";
			return response;
			
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

			return Convert.ToInt16(f * 100);


		}
		private string GetGradeAddress(Models.Responses.Validate.AddressComponent comp, string requestMode)
		{
			if (comp == null)
				return string.Empty;

			string value = comp.FormattedAddress;
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			if (value.EndsWith("USA", StringComparison.OrdinalIgnoreCase))
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
				else
				{
					sb.Append(item);
				}
			}

			return sb.ToString();




		}
	}
}

