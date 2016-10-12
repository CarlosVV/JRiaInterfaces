using CES.CoreApi.BankAccount.Api.App_Start;
using CES.CoreApi.BankAccount.Api.ExceptionHandling;
using CES.CoreApi.BankAccount.Api.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.BankAccount.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
            /*To client application auth*/
            GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.BankAccount.Api"));
            /*To return custom error messages*/
            //GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());
            /*To capture htto request message and http response message: You comment out this line if you want to stop it*/
            GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());
            /*To Register Mapper*/
            AutoMapperConfig.RegisterMappings();

            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
        }

    }
}
