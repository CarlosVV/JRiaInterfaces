using AutoMapper;
using CES.CoreApi.Receipt.Service.Business.Contract.Models;
using CES.CoreApi.Receipt.Service.Contract.Models;
using SimpleInjector;

namespace CES.CoreApi.Receipt.Service.Configuration
{
    class MapperConfigurator
    {
        public static void Configure(Container container)
        {
            Mapper.CreateMap<ReceiptModel, ReceiptResponse>().ConstructUsingServiceLocator(); ;

            Mapper.Configuration.AllowNullCollections = true;
            Mapper.Configuration.ConstructServicesUsing(container.GetInstance);
        }
    }
}
