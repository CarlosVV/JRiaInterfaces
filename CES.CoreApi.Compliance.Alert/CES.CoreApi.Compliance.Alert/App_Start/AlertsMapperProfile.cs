using AutoMapper;
using CES.CoreApi.Compliance.Alert.Business.Models;
using System.Data;

namespace CES.CoreApi.Compliance.Alert
{
	public class AlertsMapperProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<IDataReader, ReviewAlertResponse>()
					.ForMember(d => d.ResultCode, opt => opt.MapFrom(reader => int.Parse(reader["RetVal"].ToString())))
					.ForMember(d => d.Message, opt => opt.MapFrom(reader => reader["RetMsg"].ToString()));
			});
		}
	}
}
