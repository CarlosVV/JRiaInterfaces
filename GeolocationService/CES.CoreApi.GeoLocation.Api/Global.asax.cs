using CES.CoreApi.GeoLocation.Api.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
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
			GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
			/*To client application auth*/
			GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Geolocation"));
			/*To return custom error messages*/
			GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());

			GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());
			FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
		}
	}
}
