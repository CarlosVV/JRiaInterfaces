using CES.CoreApi.Agent.Service.Configuration;
using CES.CoreApi.Foundation.Service;
using SimpleInjector;

namespace CES.CoreApi.Agent.Service.Factory
{
    public class AgentServiceHostFactory : IocBasedServiceHostFactory
    {
        public AgentServiceHostFactory() 
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}
