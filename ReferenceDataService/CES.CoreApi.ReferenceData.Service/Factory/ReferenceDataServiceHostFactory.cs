using CES.CoreApi.Foundation.Service;
using CES.CoreApi.ReferenceData.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.ReferenceData.Service.Factory
{
    public class ReferenceDataServiceHostFactory : IocBasedServiceHostFactory
    {
        public ReferenceDataServiceHostFactory()
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
