using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CES.CoreApi.GeoLocation.Api.Attributes
{
	public class CoreApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(context.Exception.Message),
				ReasonPhrase = "Exception",
			});		
		}
	}
}