using AutoMapper;
using CES.CoreApi.Configuration.Model.DomainEntities;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Web
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<SettingModel, Setting>();
            Mapper.CreateMap<Setting, SettingModel>();
            Mapper.CreateMap<ServiceModel, Service>();
            Mapper.CreateMap<Service, ServiceModel>();
        }
    }
}