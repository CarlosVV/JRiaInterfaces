using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Service;
using CES.CoreApi.GeoLocation.Service.Configuration;
using CES.CoreApi.UnityProxy;

namespace CES.CoreApi.GeoLocation.Service.Factory
{
    public sealed class GeolocationServiceHostFactory : IocBasedServiceHostFactory
    {
        public GeolocationServiceHostFactory()
            : base(new UnityContainerFactory().GetInstance(InterceptionBehaviorType.Performance))
        {
            RegisterTypes();
            IocContainerConfigurator.RegisterTypes(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}