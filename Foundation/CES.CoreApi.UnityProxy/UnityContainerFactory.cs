using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace CES.CoreApi.UnityProxy
{
    public class UnityContainerFactory : IIocContainerFactory
    {
        public IIocContainer GetInstance(InterceptionBehaviorType interceptionBehaviorTypes)
        {
            var container = new UnityContainer();

            if ((interceptionBehaviorTypes & InterceptionBehaviorType.Performance) == InterceptionBehaviorType.Performance)
                container.AddNewExtension<Interception>()
                    .RegisterType<UnityPerformanceInterceptorBehavior>(new ContainerControlledLifetimeManager());

            var proxy = new UnityProxy(container);

            return proxy;
        }
    }
}