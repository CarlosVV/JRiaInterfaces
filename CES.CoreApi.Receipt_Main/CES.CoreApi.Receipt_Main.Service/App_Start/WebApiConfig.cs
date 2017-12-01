using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Web.Http;

namespace CES.CoreApi.Receipt_Main
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.UseDataContractJsonSerializer = false; // defaults to false, but no harm done
            jsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            //jsonFormatter.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;

            //Encoding oldDefault = config.Formatters.JsonFormatter.SupportedEncodings[0];
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.Add(oldDefault);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.RemoveAt(0);


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
