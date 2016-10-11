
using CES.CoreApi.Compliance.Alert.Filters;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Compliance.Alert
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			GlobalConfiguration.Configure(WebApiConfig.Register);
			GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
			GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());
			FiltersConfig.RegisterGlobalFilters();
			
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			DependenciesConfig.RegisterDependencies(container);
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
		}
    }
}
