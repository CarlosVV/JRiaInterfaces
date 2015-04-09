using CES.CoreApi.Foundation.Service;
using CES.CoreApi.GeoLocation.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.GeoLocation.Service.Factory
{
    public sealed class GeolocationServiceHostFactory : IocBasedServiceHostFactory
    {
        public GeolocationServiceHostFactory()
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}