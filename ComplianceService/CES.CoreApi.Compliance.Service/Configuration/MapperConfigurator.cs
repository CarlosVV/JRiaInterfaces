using AutoMapper;
using CES.CoreApi.Compliance.Service.Business.Contract.Models;
using CES.CoreApi.Compliance.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.Compliance.Service.Configuration
{
    class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<OrderModel, CheckOrderResponse>().ConstructUsingServiceLocator(); ;

            Mapper.Configuration.AllowNullCollections = true;
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
