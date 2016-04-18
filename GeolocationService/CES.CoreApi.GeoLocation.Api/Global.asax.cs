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

			//var container = new Container();
			//container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

			GlobalConfiguration.Configure(WebApiConfig.Register);
			var container = new Container();

			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

			// This is an extension method from the integration package.
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			CompositionRoot.RegisterDependencies(container);
			MapperConfigurator.Configure(container);
			// Register your types, for instance using the scoped lifestyle:
			

			//container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(container);




		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception exception = Server.GetLastError().GetBaseException();
		}

	}
}
