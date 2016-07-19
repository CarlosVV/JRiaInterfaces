using CES.CoreApi.GeoLocation.Api.Attributes;
using CES.CoreApi.GeoLocation.Api.Configuration;
using CES.CoreApi.GeoLocation.Api.Logger;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.GeoLocation.Api
{
	public class WebApiApplication : System.Web.HttpApplication
    {
		
        protected void Application_Start()        {
			
			GlobalConfiguration.Configure(WebApiConfig.Register);
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

			CompositionRoot.RegisterDependencies(container);
			GlobalConfiguration.Configuration.Filters.Add(new CoreApiExceptionFilterAttribute());
			GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new WebApiExceptionLogger());

			GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("GeoLocation"));

		}
	}
}
