using AutoMapper;
using CES.CoreApi.Configuration.Model.DomainEntities;
using CES.CoreApi.Configuration.Model.Models;

namespace CES.CoreApi.Configuration.Web
{
    public class MapperConfig :Profile
    {
        protected override  void Configure()
        {
			CreateMap<SettingModel, Setting>();
			CreateMap<Setting, SettingModel>();
			CreateMap<ServiceModel, Service>();
			CreateMap<Service, ServiceModel>();
		}
    }
}