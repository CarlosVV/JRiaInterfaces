using System.Collections.Generic;
using AutoMapper;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.ReferenceData.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<IEnumerable<IdentificationTypeModel>, GetIdentificationTypeListResponse>()
               .ForMember(p => p.IdentificationTypeList, map => map.MapFrom(dto => Mapper.Map<IEnumerable<IdentificationTypeModel>, IEnumerable<IdentificationTypeResponse>>(dto)))
               .ConstructUsingServiceLocator();

            Mapper.CreateMap<IdentificationTypeModel, GetIdentificationTypeResponse>()
                .ForMember(p => p.IdentificationType, map => map.MapFrom(dto => Mapper.Map<IdentificationTypeModel, IdentificationTypeResponse>(dto)))
                .ConstructUsingServiceLocator();

            Mapper.CreateMap<IdentificationTypeModel, IdentificationTypeResponse>();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<PingResponseModel, PingResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<DatabasePingModel, DatabasePingResponse>();
            
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
