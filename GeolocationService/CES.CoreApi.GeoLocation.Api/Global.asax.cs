using CES.CoreApi.GeoLocation.Api.Models;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
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
			container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
			// This is an extension method from the integration package.
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			//CompositionRoot.RegisterDependencies(container);
			CES.CoreApi.GeoLocation.Facade.Configuration.CompositionRoot.RegisterDependencies(container);
			// Register your types, for instance using the scoped lifestyle:


			//container.Verify();

			GlobalConfiguration.Configuration.DependencyResolver =
				new SimpleInjectorWebApiDependencyResolver(container);




		}

	}
}
