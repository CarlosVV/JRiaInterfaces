using CES.CoreApi.Receipt_Main.Service.App_Start;
using CES.CoreApi.Receipt_Main.Service.Config;
using CES.CoreApi.Receipt_Main.Service.ExceptionHandling;
using CES.CoreApi.Receipt_Main.Service.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ninject.Http;
using System.IO;
using System.Net.Http.Formatting;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Receipt_Main.Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private Hangfire.BackgroundJobServer _backgroundJobServer;
        protected void Application_Start()
        {
            //Encoding oldDefault = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings[0];
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.Add(oldDefault);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedEncodings.RemoveAt(0);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
            GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Receipt_Main"));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());

            AutoMapperConfig.RegisterMappings();
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
            NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);
            HangfireConfig.Start();

            // Delete temporary file storage 
            var folderKeys = HttpContext.Current.Server.MapPath($"~\\bin\\keys");
            if (Directory.Exists(folderKeys))
            {
                Directory.Delete(folderKeys, true);
            }

           _backgroundJobServer = new Hangfire.BackgroundJobServer();
        }

    }
}
