using AutoMapper;
using CES.CoreApi.Accounting.Service.Business.Contract.Interfaces;
using CES.CoreApi.Accounting.Service.Business.Contract.Models;
using CES.CoreApi.Accounting.Service.Contract.Models;
using CES.CoreApi.Common.Models;
using SimpleInjector;

namespace CES.CoreApi.Accounting.Service.Configuration
{
    internal class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<TransactionSummaryModel, GetTransactionSummaryResponse>()
               .ForMember(p => p.TransactionSummary, map => map.MapFrom(dto => Mapper.Map<TransactionSummaryModel, TransactionSummary>(dto)))
               .ConstructUsingServiceLocator();

            Mapper.CreateMap<TransactionSummaryModel, TransactionSummary>();
            Mapper.CreateMap<ClearCacheResponseModel, ClearCacheResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<HealthResponseModel, HealthMonitoringResponse>().ConstructUsingServiceLocator();
            Mapper.CreateMap<GetTransactionSummaryRequest, GetTransactionSummaryRequestModel>();
          
            //Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
