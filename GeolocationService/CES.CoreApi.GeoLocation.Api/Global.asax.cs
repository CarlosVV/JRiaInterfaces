using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.WebAPI.Filters;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Web.Http;
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
			CompositionRoot.RegisterDependencies(container);
			MapperConfigurator.Configure(container);
			GlobalConfiguration.Configuration.Filters.Add(new ApplicationAuthenticator(container.GetInstance<IApplicationAuthenticator>(), container.GetInstance<IWebApiRequestHeaderParametersService>()));
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError().GetBaseException();
		}

	}
}
