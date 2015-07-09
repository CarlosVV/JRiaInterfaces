using AutoMapper;
using CES.CoreApi.Agent.Service.Business.Contract.Models;
using CES.CoreApi.Agent.Service.Contract.Models;
using CES.CoreApi.Common.Models;
using SimpleInjector;

namespace CES.CoreApi.Agent.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<PayingAgentCurrencyModel, GetAgentCurrencyResponse>()
               .ForMember(p => p.PayingAgentCurrencyResponse, map => map.MapFrom(dto => Mapper.Map<PayingAgentCurrencyModel, PayingAgentCurrency>(dto)))
               .ConstructUsingServiceLocator();

            Mapper.CreateMap<PayingAgentCurrencyModel, PayingAgentCurrency>();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();
            Mapper.CreateMap<ProcessSignatureRequest, ProcessSignatureRequestModel>();
            Mapper.CreateMap<ProcessSignatureResponseModel, ProcessSignatureResponse>().ConstructUsingServiceLocator();
          
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
