using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Configuration.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
           // CompositionRoot.RegisterDependencies();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var monitorFactory = DependencyResolver.Current.GetService<ILogMonitorFactory>();
            if (monitorFactory == null) //if exception happened during dependency registration
                return;
            var exceptionMonitor = monitorFactory.CreateNew<IExceptionLogMonitor>();
            var exception = Server.GetLastError();
            exceptionMonitor.Publish(exception);
        }
    }
}
