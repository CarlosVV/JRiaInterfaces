using AutoMapper;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;
using CES.CoreApi.Compliance.Service.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Configuration
{
	public class MapperConfiguratorProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<OrderModel, CheckOrderResponse>();

		}
	}
}
