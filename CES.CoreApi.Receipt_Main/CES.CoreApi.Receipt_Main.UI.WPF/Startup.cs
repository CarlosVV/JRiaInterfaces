using CES.CoreApi.Receipt_Main.Service.App_Start;
using CES.CoreApi.Receipt_Main.Service.Config;
using CES.CoreApi.Receipt_Main.Service.ExceptionHandling;
using CES.CoreApi.Receipt_Main.Service.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using Ninject.Http;
using Owin;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Ninject.Web.WebApi.OwinHost;

namespace CES.CoreApi.Receipt_Main.UI.WPF
{
    public class Startup
    {
        private Hangfire.BackgroundJobServer _backgroundJobServer;
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.DependencyResolver = new NinjectHttpResolver(NinjectHttpModules.Modules);             
            config.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            AutoMapperConfig.RegisterMappings();
            FluentValidationModelValidatorProvider.Configure(config);            
            HangfireConfig.Start();
            _backgroundJobServer = new Hangfire.BackgroundJobServer();

            appBuilder.UseWebApi(config);
        }
    }
}
