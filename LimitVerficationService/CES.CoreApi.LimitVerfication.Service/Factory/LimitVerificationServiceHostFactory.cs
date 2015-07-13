using CES.CoreApi.Foundation.Service;
using CES.CoreApi.LimitVerfication.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.LimitVerfication.Service.Factory
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
