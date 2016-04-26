using CES.CoreApi.Foundation.Service;
using CES.CoreApi.Compliance.Service.Configuration;
using SimpleInjector;
namespace CES.CoreApi.Compliance.Service.Factory
{
    public class ComplianceServiceHostFactory: IocBasedServiceHostFactory
    {
        public ComplianceServiceHostFactory() 
            : base(new Container())
        {
            CompositionRoot.RegisterDependencies(Container);
          
        }
    }
}
