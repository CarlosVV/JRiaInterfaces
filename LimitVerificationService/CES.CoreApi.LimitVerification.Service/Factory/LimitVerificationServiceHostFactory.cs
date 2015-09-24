using CES.CoreApi.Foundation.Service;
using CES.CoreApi.LimitVerification.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.LimitVerification.Service.Factory
{
    public class LimitVerificationServiceHostFactory : IocBasedServiceHostFactory
    {
        public LimitVerificationServiceHostFactory()
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
