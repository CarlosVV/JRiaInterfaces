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

			var container = new Container();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			//container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
			// This is an extension method from the integration package.
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);			
			Facade.Configuration.CompositionRoot.RegisterDependencies(container);

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
