using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http.ExceptionHandling;
using System.Web.Optimization;
using CES.CoreApi.Payout.Api.Filters;
using CES.Security.CoreApi;
using System.Text;

namespace CES.CoreApi.Payout.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
			GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
			/*To client application auth*/
			GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Payout"));
			/*To return custom error messages*/
			GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionsAttribute());

			GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());

	


			BundleConfig.RegisterBundles(BundleTable.Bundles);

            AreaRegistration.RegisterAllAreas();
	

			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
			container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
			DependenciesConfig.RegisterDependencies(container);
			GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);


		}
    }
}
