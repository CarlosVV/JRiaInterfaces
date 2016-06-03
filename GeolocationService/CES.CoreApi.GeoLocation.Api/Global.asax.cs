using CES.CoreApi.GeoLocation.Facade.Configuration;
using CES.CoreApi.Logging.Attributes;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.WebApi.Interfaces;
using CES.CoreApi.Security.WebAPI.Filters;
using CES.CoreAPI.Security.WebApi.Filters;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.GeoLocation.Api
{


	public class WebApiApplication : System.Web.HttpApplication
    {
		
        protected void Application_Start()
        {
			
			GlobalConfiguration.Configure(WebApiConfig.Register);
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

			CompositionRoot.RegisterDependencies(container);
			GlobalConfiguration.Configuration.Filters.Add(new FaultExceptionFilterAttribute());
			GlobalConfiguration.Configuration.Filters.Add(new AuthenticationManager(container.GetInstance<IApplicationAuthenticator>(), container.GetInstance<IWebApiRequestHeaderParametersService>()));
			GlobalConfiguration.Configuration.Filters.Add(new AuthorizationManager(container.GetInstance<IApplicationAuthorizator>()));
			GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CES.CoreApi.Logging.Monitors.WebApiExceptionLogger());
			
		}

	}
}
