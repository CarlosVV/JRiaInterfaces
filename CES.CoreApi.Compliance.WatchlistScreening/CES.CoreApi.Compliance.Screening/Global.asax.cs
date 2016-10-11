
using CES.CoreApi.Compliance.Screening.ExceptionHandling;
using CES.CoreApi.Compliance.Screening.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Compliance.Screening
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CustomExceptionLogger());
            /*To client application auth*/
            GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Compliance.Screening"));
            /*To return custom error messages*/
            GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());

            GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());

            AutoMapperConfig.RegisterMappings();

            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
        }
    }
}
