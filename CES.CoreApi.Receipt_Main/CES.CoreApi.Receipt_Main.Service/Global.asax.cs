using CES.CoreApi.Receipt_Main.Service.App_Start;
using CES.CoreApi.Receipt_Main.Service.Config;
using CES.CoreApi.Receipt_Main.Service.ExceptionHandling;
using CES.CoreApi.Receipt_Main.Service.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using Ninject.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Receipt_Main.Service
{    
    public class WebApiApplication : System.Web.HttpApplication
    {
        private Hangfire.BackgroundJobServer _backgroundJobServer;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
            GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Receipt_Main"));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            AutoMapperConfig.RegisterMappings();
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
            NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);
            HangfireConfig.Start();

            _backgroundJobServer = new Hangfire.BackgroundJobServer();
        }

    }
}
