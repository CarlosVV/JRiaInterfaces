using AutoMapper;
using CES.CoreApi.Agent.Service.Business.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Agent.Service.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Contract.Models;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Shared.Business.Contract.Enumerations;
using CES.CoreApi.Shared.Business.Contract.Models.Agents;
using CES.CoreApi.Shared.Business.Contract.Models.Common;
using SimpleInjector;

namespace CES.CoreApi.Agent.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<PayingAgentModel, GetPayingAgentResponse>()
               .ForMember(p => p.PayingAgent, map => map.MapFrom(dto => Mapper.Map<PayingAgentModel, PayingAgent>(dto)))
               .ConstructUsingServiceLocator();
            
            Mapper.CreateMap<PayingAgentModel, PayingAgent>();
            Mapper.CreateMap<PayingAgentCurrencyModel, AgentCurrency>();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();
            Mapper.CreateMap<ProcessSignatureRequest, ProcessSignatureRequestModel>();
            Mapper.CreateMap<ProcessSignatureResponseModel, ProcessSignatureResponse>().ConstructUsingServiceLocator();

            Mapper.CreateMap<AddressModel, Address>();
            Mapper.CreateMap<TelephoneModel, Phone>();
            Mapper.CreateMap<ContactModel, Contact>();
            Mapper.CreateMap<GeolocationModel, Geolocation>();
            Mapper.CreateMap<PayingAgentLocationModel, AgentLocation>();

            Mapper.CreateMap<PayingAgentInformationGroup, PayingAgentDetalizationLevel>();
            Mapper.CreateMap<AddressValidationResult, string>()
                .ConvertUsing(src => src.ToString());
            Mapper.CreateMap<PhoneType, string>()
                .ConvertUsing(src => src == default(PhoneType) ? null : src.ToString());

            Mapper.Configuration.AllowNullCollections = true;
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
