using CES.CoreApi.Accounting.Service.Configuration;
using CES.CoreApi.Foundation.Service;
using SimpleInjector;

namespace CES.CoreApi.Accounting.Service.Factory
{
    public class AccountingServiceHostFactory : IocBasedServiceHostFactory
    {
        public AccountingServiceHostFactory() 
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
