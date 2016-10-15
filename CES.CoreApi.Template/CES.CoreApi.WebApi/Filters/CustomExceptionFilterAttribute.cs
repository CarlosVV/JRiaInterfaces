using CES.CoreApi.WebApi.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http.Filters;

namespace CES.CoreApi.WebApi.Filters
{
	/// <summary>
	/// Exception Filter: This is to response  a customize error message and HttpStatusCode code.
	/// Optional: You can customize the exception message
	///			 You can send exception with custom message
	///          Or You can only return custom message
	public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			var statusCode = HttpStatusCode.InternalServerError;

			if (context.Exception is NotImplementedException)
				statusCode = HttpStatusCode.NotImplemented;

			else if (context.Exception is AuthenticationException)
				statusCode = HttpStatusCode.Unauthorized;
		
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