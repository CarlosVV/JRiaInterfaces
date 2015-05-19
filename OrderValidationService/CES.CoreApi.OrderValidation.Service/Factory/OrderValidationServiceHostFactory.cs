using CES.CoreApi.Foundation.Service;
using CES.CoreApi.OrderValidation.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.OrderValidation.Service.Factory
{
    public sealed class OrderValidationServiceHostFactory : IocBasedServiceHostFactory
    {
        public OrderValidationServiceHostFactory()
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
