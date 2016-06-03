using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CES.CoreApi.Logging.Attributes
{
	public class FaultExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			//if (context.Exception is FormatException)
			//{
				//context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
				//context.Exception = new Exception(context.Exception.StackTrace);
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(context.Exception.Message),
					ReasonPhrase = "Exception",
					
				});
			//}
		}
	}
}
