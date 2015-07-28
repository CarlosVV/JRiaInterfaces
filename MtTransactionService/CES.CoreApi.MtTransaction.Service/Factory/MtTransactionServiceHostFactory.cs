using CES.CoreApi.Foundation.Service;
using CES.CoreApi.MtTransaction.Service.Configuration;
using SimpleInjector;

namespace CES.CoreApi.MtTransaction.Service.Factory
{
    public class MtTransactionServiceHostFactory : IocBasedServiceHostFactory
    {
        public MtTransactionServiceHostFactory()
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
            MapperConfigurator.Configure(Container);
        }
    }
}