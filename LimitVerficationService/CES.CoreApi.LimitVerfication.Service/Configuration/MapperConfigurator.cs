using System.Collections.Generic;
using AutoMapper;
using SimpleInjector;

namespace CES.CoreApi.LimitVerfication.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            //Mapper.CreateMap<IEnumerable<IdentificationTypeModel>, GetIdentificationTypeListResponse>()
            //   .ForMember(p => p.IdentificationTypeList, map => map.MapFrom(dto => Mapper.Map<IEnumerable<IdentificationTypeModel>, IEnumerable<IdentificationTypeResponse>>(dto)))
            //   .ConstructUsingServiceLocator();

            //Mapper.CreateMap<IdentificationTypeModel, GetIdentificationTypeResponse>()
            //    .ForMember(p => p.IdentificationType, map => map.MapFrom(dto => Mapper.Map<IdentificationTypeModel, IdentificationTypeResponse>(dto)))
            //    .ConstructUsingServiceLocator();

            //Mapper.CreateMap<IdentificationTypeModel, IdentificationTypeResponse>();
            //Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            //Mapper.CreateMap<HealthResponseModel, HealthResponse>().ConstructUsingServiceLocator();
            
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
