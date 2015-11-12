namespace CES.CoreApi.Customer.Service.App_Start
{
    using System.Reflection;

    using SimpleInjector;
    using SimpleInjector.Integration.Wcf;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it for the WCF ServiceHostFactory.</summary>
        public static void Initialize()
        {
            var container = new Container();

            InitializeContainer(container);

            container.RegisterWcfServices(Assembly.GetExecutingAssembly());

            SimpleInjectorServiceHostFactory.SetContainer(container);

            // TODO: Add the following attribute to all .svc files:
            // Factory="SimpleInjector.Integration.Wcf.SimpleInjectorServiceHostFactory, SimpleInjector.Integration.Wcf"
        }

        private static void InitializeContainer(Container container)
        {
            //Register your services here (remove this line).

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>();
        }
    }
}