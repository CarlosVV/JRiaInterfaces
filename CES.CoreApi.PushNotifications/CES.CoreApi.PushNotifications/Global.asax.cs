
using CES.CoreApi.PushNotifications.Filters;
using CES.Security.CoreApi;
using FluentValidation.WebApi;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.PushNotifications
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CoreApiExceptionLogger());
			/*To client application auth*/
			GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.PushNotifications"));
			/*To capture htto request message and http response message: You comment out this line if you want to stop it*/
			GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());

			/*To return custom error messages*/
			GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());

			FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);
		}
	}
}
