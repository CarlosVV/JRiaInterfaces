using CES.CoreApi.Foundation.Service;
using CES.CoreApi.Receipt.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.Receipt.Service.Factory
{
    public class ReceiptServiceHostFactory: IocBasedServiceHostFactory
    {
        public ReceiptServiceHostFactory() 
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
