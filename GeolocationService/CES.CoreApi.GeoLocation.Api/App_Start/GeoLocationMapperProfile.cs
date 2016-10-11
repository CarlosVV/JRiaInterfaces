using AutoMapper;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;



namespace CES.CoreApi.GeoLocation.Api
{
	public class GeoLocationMapperProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<AddressRequest, AddressModel>();
			CreateMap<AddressRequest, AutocompleteAddressModel>();
			CreateMap<AddressModel, AddressRequest>();
			CreateMap<AddressModel, ValidatedAddress>();
			CreateMap<AddressModel, AutocompleteAddress>();
			CreateMap<AddressModel, GeocodeAddress>();
			CreateMap<AutocompleteSuggestionModel, AutocompleteSuggestion>();
			CreateMap<Location, LocationModel>();
			CreateMap<MapSize, MapSizeModel>();
			CreateMap<PushPin, PushPinModel>();
			CreateMap<MapOutputParameters, MapOutputParametersModel>();
			CreateMap<Confidence, LevelOfConfidence>();
			CreateMap<LevelOfConfidence, Confidence>();
			CreateMap<PinColor, Color>();
			CreateMap<DataProviderType, DataProvider>();
			CreateMap<LocationModel, Location>();
			CreateMap<ValidateAddressResponseModel, ValidateAddressResponse>().ConstructUsingServiceLocator();
			CreateMap<AutocompleteAddressResponseModel, AutocompleteAddressResponse>().ConstructUsingServiceLocator();
			CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
			CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
			CreateMap<GetMapResponseModel, GetMapResponse>().ConstructUsingServiceLocator();
		}
	}

	
}
