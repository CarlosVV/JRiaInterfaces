using CES.CoreApi.GeoLocation.Api.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Filters;

namespace CES.CoreApi.GeoLocation.Api.Filters
{
	public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			var statusCode = HttpStatusCode.InternalServerError;

			if (context.Exception is NotImplementedException)
				statusCode = HttpStatusCode.NotImplemented;

			else if (context.Exception is AuthenticationException)
				statusCode = HttpStatusCode.Unauthorized;
			/*
			//context.Response = context.Request.CreateErrorResponse(statusCode, context.Exception);
			*/
			var response = new
			{
				Code = 9999,
				Message = context.Exception.Message,
				ResponseId = Client.GetCorrelationId(context.Request)
			};
			context.Response = context.Request.CreateResponse(statusCode, response, "application/json");

		}
	}
}