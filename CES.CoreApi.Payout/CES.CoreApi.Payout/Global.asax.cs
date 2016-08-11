using CES.CoreApi.Payout.App_Start;
using CES.CoreApi.Payout.ExceptionHandling;
using CES.CoreApi.Payout.Filters;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Payout
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), new CoreApiExceptionLogger());
			/*To client application auth*/
			GlobalConfiguration.Configuration.Filters.Add(new AuthenticationFilter("CES.CoreApi.Payout"));
			/*To return custom error messages*/
			GlobalConfiguration.Configuration.Filters.Add(new CustomExceptionFilterAttribute());
			/*To capture htto request message and http response message: You comment out this line if you want to stop it*/
			GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMessageHandler());
			AutoMapperConfig.RegisterMappings();
		}
	}
}
