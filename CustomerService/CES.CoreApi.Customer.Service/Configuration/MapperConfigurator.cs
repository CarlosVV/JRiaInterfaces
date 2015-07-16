using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Business.Contract.Models;
using CES.CoreApi.Customer.Service.Contract.Enumerations;
using CES.CoreApi.Customer.Service.Contract.Models;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.Customer.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<AddressModel, Address>();
            Mapper.CreateMap<TelephoneModel, Telephone>();
            Mapper.CreateMap<NameModel, Name>();
            Mapper.CreateMap<ContactModel, Contact>();
            Mapper.CreateMap<GeolocationModel, Geolocation>();
            Mapper.CreateMap<AddressValidationResult, AddressValidationStatus>();
            Mapper.CreateMap<TelephoneKind, TelephoneType>();
            Mapper.CreateMap<CustomerModel, Contract.Models.Customer>();

            Mapper.CreateMap<CustomerModel, CustomerGetResponse>()
               .ForMember(p => p.Customer, map => map.MapFrom(dto => Mapper.Map<CustomerModel, Contract.Models.Customer>(dto)))
               .ConstructUsingServiceLocator();

            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();
            Mapper.CreateMap<ProcessSignatureModel, CustomerProcessSignatureResponse>().ConstructUsingServiceLocator();

            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
