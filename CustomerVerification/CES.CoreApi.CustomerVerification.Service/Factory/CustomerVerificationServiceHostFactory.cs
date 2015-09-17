using CES.CoreApi.CustomerVerification.Service.Configuration;
using CES.CoreApi.Foundation.Service;
using SimpleInjector;

namespace CES.CoreApi.CustomerVerification.Service.Factory
{
    public class CustomerVerificationServiceHostFactory : IocBasedServiceHostFactory
    {
        public CustomerVerificationServiceHostFactory() 
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
