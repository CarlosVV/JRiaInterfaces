using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Configuration.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof (IExceptionLogger));

            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionLogger), logger);
            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
