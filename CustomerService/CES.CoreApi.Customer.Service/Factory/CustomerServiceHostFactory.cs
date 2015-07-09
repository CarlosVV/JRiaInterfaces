using CES.CoreApi.Customer.Service.Configuration;
using CES.CoreApi.Foundation.Service;
using SimpleInjector;

namespace CES.CoreApi.Customer.Service.Factory
{
    public class CustomerServiceHostFactory : IocBasedServiceHostFactory
    {
        public CustomerServiceHostFactory() 
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
