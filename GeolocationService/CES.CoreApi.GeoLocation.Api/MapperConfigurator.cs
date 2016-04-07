using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Data.Models;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.GeoLocation.Api
{
    public class MapperConfigurator
    {
		public static void Configure(Container container)
		{
			var config = new MapperConfiguration(cfg =>
			{



				cfg.CreateMap<AddressRequest, AddressModel>();
				cfg.CreateMap<AddressRequest, AutocompleteAddressModel>();
				cfg.CreateMap<AddressModel, AddressRequest>();
				cfg.CreateMap<AddressModel, ValidatedAddress>();
				cfg.CreateMap<AddressModel, AutocompleteAddress>();
				cfg.CreateMap<AddressModel, GeocodeAddress>();
				cfg.CreateMap<AutocompleteSuggestionModel, AutocompleteSuggestion>();
				cfg.CreateMap<Location, LocationModel>();
				cfg.CreateMap<MapSize, MapSizeModel>();
				cfg.CreateMap<PushPin, PushPinModel>();
				cfg.CreateMap<MapOutputParameters, MapOutputParametersModel>();
				cfg.CreateMap<Confidence, LevelOfConfidence>();
				cfg.CreateMap<LevelOfConfidence, Confidence>();
				cfg.CreateMap<PinColor, Color>();
				cfg.CreateMap<DataProviderType, DataProvider>();
				cfg.CreateMap<LocationModel, Location>();
				cfg.CreateMap<ValidateAddressResponseModel, ValidateAddressResponse>().ConstructUsingServiceLocator();
				cfg.CreateMap<AutocompleteAddressResponseModel, AutocompleteAddressResponse>().ConstructUsingServiceLocator();
				cfg.CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
				cfg.CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
				cfg.CreateMap<GetMapResponseModel, GetMapResponse>().ConstructUsingServiceLocator();
				cfg.CreateMap<GetProviderKeyResponseModel, GetProviderKeyResponse>().ConstructUsingServiceLocator();

				cfg.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
				cfg.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
				cfg.CreateMap<DatabasePingModel, DatabasePingResponse>();


				cfg.ConstructServicesUsing(container.GetInstance);
			});
			////Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
			//Mapper.CreateMap<AddressRequest, AddressModel>();
			//Mapper.CreateMap<AddressRequest, AutocompleteAddressModel>();
			//Mapper.CreateMap<AddressModel, AddressRequest>();
			//Mapper.CreateMap<AddressModel, ValidatedAddress>();
			//Mapper.CreateMap<AddressModel, AutocompleteAddress>();
			//Mapper.CreateMap<AddressModel, GeocodeAddress>();
			//Mapper.CreateMap<AutocompleteSuggestionModel, AutocompleteSuggestion>();
			//Mapper.CreateMap<Location, LocationModel>();
			//Mapper.CreateMap<MapSize, MapSizeModel>();
			//Mapper.CreateMap<PushPin, PushPinModel>();
			//Mapper.CreateMap<MapOutputParameters, MapOutputParametersModel>();
			//Mapper.CreateMap<Confidence, LevelOfConfidence>();
			//Mapper.CreateMap<LevelOfConfidence, Confidence>();
			//Mapper.CreateMap<PinColor, Color>();
			//Mapper.CreateMap<DataProviderType, DataProvider>();
			//Mapper.CreateMap<LocationModel, Location>();
			//Mapper.CreateMap<ValidateAddressResponseModel, ValidateAddressResponse>().ConstructUsingServiceLocator();
			//Mapper.CreateMap<AutocompleteAddressResponseModel, AutocompleteAddressResponse>().ConstructUsingServiceLocator();
			//Mapper.CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
			//Mapper.CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
			//Mapper.CreateMap<GetMapResponseModel, GetMapResponse>().ConstructUsingServiceLocator();
			//Mapper.CreateMap<GetProviderKeyResponseModel, GetProviderKeyResponse>().ConstructUsingServiceLocator();

			//Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
			//Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
			//Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();

			//Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
		}
    }
}
