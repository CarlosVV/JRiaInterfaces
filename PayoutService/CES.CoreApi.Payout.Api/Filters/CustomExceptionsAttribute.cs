using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web;
using System.Web.Http.Filters;

namespace CES.CoreApi.Payout.Api.Filters
{
	public class CustomExceptionsAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			var statusCode = HttpStatusCode.InternalServerError;

			if (context.Exception is NotImplementedException)
				statusCode = HttpStatusCode.NotImplemented;

			else if (context.Exception is AuthenticationException)
				statusCode = HttpStatusCode.Unauthorized;


			var message =
															 new
															 {
																 ResultCode = statusCode,
																 Message = context.Exception == null ? "" : context.Exception.Message,
																 ResponseId = context.Request.GetCorrelationId()
															 };

			context.Response = context.Request.CreateResponse(statusCode, message, "application/json");

		}
	}
}