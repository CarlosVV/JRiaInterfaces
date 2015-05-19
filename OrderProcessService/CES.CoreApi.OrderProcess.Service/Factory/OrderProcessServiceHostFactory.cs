using CES.CoreApi.Foundation.Service;
using CES.CoreApi.OrderProcess.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.OrderProcess.Service.Factory
{
    public class OrderProcessServiceHostFactory : IocBasedServiceHostFactory
    {
        public OrderProcessServiceHostFactory()
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}