using System.ServiceProcess;
using CES.AddressVerification.Service.Configuration;
using SimpleInjector;

namespace CES.AddressVerification.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var container = new Container();
            CompositionRoot.RegisterDependencies(container);

            var servicesToRun = new ServiceBase[] 
            {
                container.GetInstance<AddressVerificationService>()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
