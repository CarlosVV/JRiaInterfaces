using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Business.Contract.Models;
using CES.CoreApi.CustomerVerification.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.CustomerVerification.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<CustomerName, CustomerNameModel>();
            Mapper.CreateMap<VerifyCustomerIdentityRequest, VerifyCustomerIdentityRequestModel>();
            //Mapper.CreateMap<NameModel, Name>();
            //Mapper.CreateMap<ContactModel, Contact>();
            //Mapper.CreateMap<GeolocationModel, Geolocation>();
            //Mapper.CreateMap<CustomerModel, System.Diagnostics.Contracts.Contract.Models.Customer>();

            //Mapper.CreateMap<CustomerModel, CustomerGetResponse>()
            //   .ForMember(p => p.Customer, map => map.MapFrom(dto => Mapper.Map<CustomerModel, System.Diagnostics.Contracts.Contract.Models.Customer>(dto)))
            //   .ConstructUsingServiceLocator();

            //Mapper.CreateMap<GenderEnum, string>()
            //   .ConvertUsing(src => src == default(GenderEnum) ? null : src.ToString());
            //Mapper.CreateMap<AddressValidationResult, string>()
            //    .ConvertUsing(src => src.ToString());
            //Mapper.CreateMap<AgentTypeEnum, string>()
            //    .ConvertUsing(src => src == default(AgentTypeEnum) ? null : src.ToString());
            //Mapper.CreateMap<DeliveryMethodType, string>()
            //    .ConvertUsing(src => src == default(DeliveryMethodType) ? null : src.ToString());
            //Mapper.CreateMap<PhoneType, string>()
            //    .ConvertUsing(src => src == default(PhoneType) ? null : src.ToString());

            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();
            //Mapper.CreateMap<ProcessSignatureModel, CustomerProcessSignatureResponse>().ConstructUsingServiceLocator();

            Mapper.Configuration.AllowNullCollections = true;
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
