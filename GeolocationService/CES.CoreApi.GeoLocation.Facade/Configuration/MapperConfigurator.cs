using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Models;


namespace CES.CoreApi.GeoLocation.Facade.Configuration
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
			CreateMap<GetProviderKeyResponseModel, GetProviderKeyResponse>().ConstructUsingServiceLocator();
			CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
			CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
			CreateMap<DatabasePingModel, DatabasePingResponse>();           //Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
		}
	}

	
}
