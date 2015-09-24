using SimpleInjector;

namespace CES.AddressVerification.Service.Configuration
{
    internal class CompositionRoot
    {
        public static void RegisterDependencies(Container container)
        {

            container.Verify();
        }
    }
}
