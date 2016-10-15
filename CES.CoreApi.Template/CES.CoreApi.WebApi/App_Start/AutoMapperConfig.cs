using AutoMapper;
using CES.CoreApi.WebApi.Models;
using CES.CoreApi.WebApi.ViewModels;

namespace CES.CoreApi.WebApi.App_Start
{
	public static class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
			Mapper.CreateMap<ServiceOfferCurrencyRequest, CountryCurrencyRequest>();
		}
	}
}