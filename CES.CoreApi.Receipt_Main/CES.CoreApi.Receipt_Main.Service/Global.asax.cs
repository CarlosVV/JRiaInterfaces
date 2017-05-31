using CES.CoreApi.Receipt_Main.Service.App_Start;
using CES.CoreApi.Receipt_Main.Service.ExceptionHandling;
using CES.CoreApi.Receipt_Main.Service.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using Ninject.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Receipt_Main
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
            /*To client application auth*/
            GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Receipt_Main"));
            /*To return custom error messages*/
            //GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());
            /*To capture htto request message and http response message: You comment out this line if you want to stop it*/
            GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());
            /*To Register Mapper*/
            AutoMapperConfig.RegisterMappings();

            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);

            NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);
        }

    }
}
