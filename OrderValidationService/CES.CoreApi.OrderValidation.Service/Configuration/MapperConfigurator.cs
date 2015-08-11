using AutoMapper;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using CES.CoreApi.OrderValidation.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.OrderValidation.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<OrderValidateRequest, TransactionValidateRequestModel>();
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
            //Mapper.CreateMap<HealthResponseModel, HealthResponse>().ConstructUsingServiceLocator();
            //Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            //Mapper.CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
            //Mapper.CreateMap<GeocodeAddressResponseModel, GeocodeAddressResponse>().ConstructUsingServiceLocator();
            //Mapper.CreateMap<GetMapResponseModel, GetMapResponse>().ConstructUsingServiceLocator();
            //Mapper.CreateMap<GetProviderKeyResponseModel, GetProviderKeyResponse>().ConstructUsingServiceLocator();

            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
