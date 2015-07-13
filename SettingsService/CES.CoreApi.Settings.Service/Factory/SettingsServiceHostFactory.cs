using CES.CoreApi.Foundation.Service;
using CES.CoreApi.Settings.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.Settings.Service.Factory
{
    public class SettingsServiceHostFactory : IocBasedServiceHostFactory
    {
        public SettingsServiceHostFactory()
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
